-- =================================================================================
-- EXTENSÃO PARA GERAÇÃO DE UUID
-- =================================================================================
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- =================================================================================
-- FUNÇÃO PARA ATUALIZAR updated_at AUTOMATICAMENTE
-- =================================================================================
CREATE OR REPLACE FUNCTION set_updated_at()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = now();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- =================================================================================
-- TABELAS DE LOOKUP PARA STATUS
-- =================================================================================
CREATE TABLE order_statuses (
    id   SERIAL PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE payment_statuses (
    id   SERIAL PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE shipment_statuses (
    id   SERIAL PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- =================================================================================
-- TABELAS PRINCIPAIS COM SOFT DELETE EM PEDIDOS E CARRINHOS
-- =================================================================================

CREATE TABLE customers (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name        VARCHAR(255) NOT NULL,
    email       VARCHAR(255) NOT NULL UNIQUE,
    password    VARCHAR(255) NOT NULL,
    cpf         CHAR(11) UNIQUE,
    deleted_at  TIMESTAMP NULL,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT ck_customers_cpf_format  CHECK (cpf IS NULL OR cpf ~ '^[0-9]{11}$'),
    CONSTRAINT ck_customers_email_lower CHECK (email = lower(email))
);
CREATE INDEX ix_customers_email ON customers(email);
CREATE INDEX ix_customers_cpf   ON customers(cpf);

CREATE TABLE customer_addresses (
    id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id  UUID NOT NULL,
    street       VARCHAR(255) NOT NULL,
    number       VARCHAR(50) NOT NULL,
    city         VARCHAR(100) NOT NULL,
    state        VARCHAR(50) NOT NULL,
    zip_code     VARCHAR(20) NOT NULL,
    created_at   TIMESTAMP NOT NULL DEFAULT now(),
    updated_at   TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_addr_customer FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);
CREATE INDEX ix_addr_customer_id ON customer_addresses(customer_id);

CREATE TABLE categories (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name        VARCHAR(100) NOT NULL UNIQUE,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT ck_categories_name_not_empty CHECK (char_length(name) > 0)
);

CREATE TABLE products (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name            VARCHAR(150) NOT NULL,
    description     TEXT NOT NULL,
    price           NUMERIC(18,2) NOT NULL,
    old_price       NUMERIC(18,2),
    category_id     UUID NOT NULL,
    stock_quantity  INT NOT NULL,
    sold            INT NOT NULL DEFAULT 0,
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    popular         BOOLEAN NOT NULL DEFAULT FALSE,
    created_at      TIMESTAMP NOT NULL DEFAULT now(),
    updated_at      TIMESTAMP NOT NULL DEFAULT now(),
    deleted_at      TIMESTAMP,
    CONSTRAINT fk_products_category FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE RESTRICT,
    CONSTRAINT ck_product_name CHECK (char_length(name) BETWEEN 3 AND 150),
    CONSTRAINT ck_product_description CHECK (char_length(description) BETWEEN 10 AND 1000),
    CONSTRAINT ck_product_price CHECK (price > 0),
    CONSTRAINT ck_product_old_price CHECK (old_price IS NULL OR old_price >= 0),
    CONSTRAINT ck_product_stock CHECK (stock_quantity >= 0),
    CONSTRAINT ck_product_sold CHECK (sold >= 0),
    CONSTRAINT ck_product_dates CHECK (updated_at >= created_at)
);
CREATE INDEX ix_products_category ON products(category_id);

CREATE TABLE product_images (
    id         UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id UUID NOT NULL,
    url        TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_images_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
);
CREATE INDEX ix_images_product_id ON product_images(product_id);

CREATE TABLE colors (
    id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name         VARCHAR(50) NOT NULL UNIQUE,
    value        VARCHAR(20) NOT NULL,
    created_at   TIMESTAMP NOT NULL DEFAULT now(),
    updated_at   TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT ck_colors_name_not_empty  CHECK (char_length(name) > 0),
    CONSTRAINT ck_colors_value_format    CHECK (
        value ~* '^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$'
        OR value ~* '^[A-Za-z]+'
    )
);
CREATE INDEX ix_colors_value ON colors(value);

CREATE TABLE product_colors (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id  UUID NOT NULL,
    color_id    UUID NOT NULL,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_prodcolor_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE,
    CONSTRAINT fk_prodcolor_color   FOREIGN KEY (color_id)   REFERENCES colors(id)   ON DELETE RESTRICT
);
CREATE UNIQUE INDEX uq_product_color ON product_colors(product_id, color_id);
CREATE INDEX ix_prodcolor_product ON product_colors(product_id);
CREATE INDEX ix_prodcolor_color   ON product_colors(color_id);

CREATE TABLE sizes (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name        VARCHAR(50) NOT NULL UNIQUE,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT ck_sizes_name_not_empty CHECK (char_length(name) > 0)
);

CREATE INDEX ix_sizes_name ON sizes(name);

CREATE TABLE product_size (
    id             UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id     UUID NOT NULL,
    size_id        UUID NOT NULL,
    stock_quantity INT NOT NULL DEFAULT 0,
    created_at     TIMESTAMP NOT NULL DEFAULT now(),
    updated_at     TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_productsize_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE,
    CONSTRAINT fk_productsize_size    FOREIGN KEY (size_id)    REFERENCES sizes(id)    ON DELETE RESTRICT,
    CONSTRAINT ck_productsize_stock   CHECK (stock_quantity >= 0),
    CONSTRAINT uq_productsize UNIQUE (product_id, size_id)
);

CREATE INDEX ix_productsize_product ON product_size(product_id);
CREATE INDEX ix_productsize_size    ON product_size(size_id);


-- =================================================================================
-- HISTÓRICO DE PREÇOS DE PRODUTOS
-- =================================================================================
CREATE TABLE product_price_history (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id  UUID NOT NULL,
    old_price   NUMERIC(18,2) NOT NULL,
    new_price   NUMERIC(18,2) NOT NULL,
    changed_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_pricehist_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
);
CREATE INDEX ix_pricehist_product ON product_price_history(product_id);

-- =================================================================================
-- CHECKOUT E PROCESSAMENTO (com soft delete em orders)
-- =================================================================================
CREATE TABLE orders (
    id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id         UUID NOT NULL,
    shipping_address_id UUID NOT NULL,
    coupon_id           UUID,
    status_id           INT NOT NULL REFERENCES order_statuses(id),
    total_amount        NUMERIC(18,2) NOT NULL,
    placed_at           TIMESTAMP NOT NULL DEFAULT now(),
    shipped_at          TIMESTAMP,
    delivered_at        TIMESTAMP,
    deleted_at          TIMESTAMP,
    created_at          TIMESTAMP NOT NULL DEFAULT now(),
    updated_at          TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_orders_customer FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE RESTRICT,
    CONSTRAINT fk_orders_address FOREIGN KEY (shipping_address_id) REFERENCES customer_addresses(id) ON DELETE RESTRICT,
    CONSTRAINT fk_orders_coupon FOREIGN KEY (coupon_id) REFERENCES coupons(id) ON DELETE SET NULL
);
CREATE INDEX ix_orders_customer ON orders(customer_id);

CREATE TABLE order_items (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id    UUID NOT NULL,
    product_id  UUID NOT NULL,
    quantity    INT NOT NULL CHECK (quantity > 0),
    unit_price  NUMERIC(18,2) NOT NULL,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_items_order FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
    CONSTRAINT fk_items_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE RESTRICT
);
CREATE INDEX ix_items_order ON order_items(order_id);
CREATE INDEX ix_items_product ON order_items(product_id);

CREATE TABLE payments (
    id             UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id       UUID NOT NULL,
    status_id      INT NOT NULL REFERENCES payment_statuses(id),
    method         VARCHAR(50) NOT NULL,
    amount         NUMERIC(18,2) NOT NULL,
    transaction_id VARCHAR(100),
    paid_at        TIMESTAMP,
    created_at     TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_payments_order FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE
);
CREATE INDEX ix_payments_order ON payments(order_id);

CREATE TABLE shipments (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id        UUID NOT NULL,
    status_id       INT NOT NULL REFERENCES shipment_statuses(id),
    carrier         VARCHAR(100) NOT NULL,
    tracking_number VARCHAR(100) NOT NULL,
    shipped_at      TIMESTAMP,
    delivered_at    TIMESTAMP,
    created_at      TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_shipments_order FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE
);
CREATE INDEX ix_shipments_order ON shipments(order_id);

-- =================================================================================
-- CARRINHO DE COMPRAS (com soft delete no carrinho)
-- =================================================================================
CREATE TABLE carts (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id UUID NOT NULL UNIQUE,
    deleted_at  TIMESTAMP,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_carts_customer FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);

CREATE TABLE cart_items (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    cart_id     UUID NOT NULL,
    product_id  UUID NOT NULL,
    quantity    INT NOT NULL CHECK (quantity > 0),
    unit_price  NUMERIC(18,2) NOT NULL,
    color_id    UUID NULL,
    added_at    TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_cartitems_cart     FOREIGN KEY (cart_id)    REFERENCES carts(id)            ON DELETE CASCADE,
    CONSTRAINT fk_cartitems_product  FOREIGN KEY (product_id) REFERENCES products(id)         ON DELETE RESTRICT,
    CONSTRAINT fk_cartitems_color    FOREIGN KEY (color_id)   REFERENCES product_colors(id) ON DELETE SET NULL
);
CREATE INDEX ix_cartitems_cart    ON cart_items(cart_id);
CREATE INDEX ix_cartitems_product ON cart_items(product_id);
CREATE INDEX ix_cartitems_color   ON cart_items(color_id);

-- =================================================================================
-- CUPONS E ATRIBUIÇÕES
-- =================================================================================
CREATE TABLE coupons (
    id             UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    code           VARCHAR(50) NOT NULL UNIQUE,
    discount_type  VARCHAR(20) NOT NULL,
    discount_value NUMERIC(18,2) NOT NULL,
    valid_from     DATE NOT NULL,
    valid_to       DATE NOT NULL,
    usage_count    INT NOT NULL DEFAULT 0,
    max_usage      INT,
    created_at     TIMESTAMP NOT NULL DEFAULT now(),
    updated_at     TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT ck_coupons_dates CHECK (valid_to >= valid_from),
    CONSTRAINT ck_coupons_value CHECK (
        (discount_type = 'percent' AND discount_value BETWEEN 0 AND 100)
        OR (discount_type = 'fixed' AND discount_value >= 0)
    )
);

CREATE TABLE customer_coupons (
    id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id  UUID NOT NULL,
    coupon_id    UUID NOT NULL,
    assigned_at  TIMESTAMP NOT NULL DEFAULT now(),
    redeemed_at  TIMESTAMP,
    CONSTRAINT fk_custcoup_customer FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE,
    CONSTRAINT fk_custcoup_coupon FOREIGN KEY (coupon_id) REFERENCES coupons(id) ON DELETE CASCADE,
    CONSTRAINT uq_customer_coupon UNIQUE (customer_id, coupon_id)
);
CREATE INDEX ix_custcoup_customer ON customer_coupons(customer_id);
CREATE INDEX ix_custcoup_coupon ON customer_coupons(coupon_id);

-- =================================================================================
-- AVALIAÇÕES DE PRODUTO
-- =================================================================================
CREATE TABLE product_reviews (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id  UUID NOT NULL,
    customer_id UUID NOT NULL,
    rating      INT NOT NULL CHECK (rating BETWEEN 1 AND 5),
    comment     TEXT,
    created_at  TIMESTAMP NOT NULL DEFAULT now(),
    updated_at  TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_reviews_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE,
    CONSTRAINT fk_reviews_customer FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);
CREATE INDEX ix_reviews_product ON product_reviews(product_id);
CREATE INDEX ix_reviews_customer ON product_reviews(customer_id);

-- =================================================================================
-- TRIGGERS PARA ATUALIZAR updated_at AUTOMATICAMENTE
-- =================================================================================
DO $$
BEGIN
    FOR tbl IN ARRAY[
        'customers', 'customer_addresses', 'categories',
        'products', 'orders', 'customer_coupons', 'product_reviews',
        'carts', 'product_colors', 'colors', 'coupons'
    ]
    LOOP
        EXECUTE format('
            DROP TRIGGER IF EXISTS trg_%1$s_updated_at ON %1$s;
            CREATE TRIGGER trg_%1$s_updated_at
            BEFORE UPDATE ON %1$s
            FOR EACH ROW
            EXECUTE FUNCTION set_updated_at();',
            tbl
        );
    END LOOP;
END;
$$;  

-- =================================================================================
-- MATERIALIZED VIEWS PARA RELATÓRIOS
-- =================================================================================

-- Vendas totais por categoria
CREATE MATERIALIZED VIEW sales_by_category AS
SELECT
    c.id AS category_id,
    c.name AS category_name,
    SUM(oi.unit_price * oi.quantity) AS total_sales
FROM order_items oi
JOIN products p ON oi.product_id = p.id
JOIN categories c ON p.category_id = c.id
JOIN orders o ON oi.order_id = o.id AND o.deleted_at IS NULL
GROUP BY c.id, c.name;

-- Estoque baixo (produtos com quantidade menor que threshold)
CREATE MATERIALIZED VIEW low_stock_products AS
SELECT
    id AS product_id,
    name,
    stock_quantity
FROM products
WHERE stock_quantity < 10 AND deleted_at IS NULL;

-- Atualizar views manualmente conforme necessidade
-- REFRESH MATERIALIZED VIEW sales_by_category;
-- REFRESH MATERIALIZED_VIEW low_stock_products;





























INSERT INTO categories (id, name, created_at, updated_at) VALUES
  ('a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 'Smartphones', NOW(), NOW()),
  ('c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 'Cameras', NOW(), NOW()),
  ('abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 'Mobiles', NOW(), NOW()),
  ('b3d93042-4782-4c5d-b250-6018c43834f5', 'Speakers', NOW(), NOW()),
  ('d14a17fc-e499-4d55-988c-5e96e28a94f2', 'Mouse', NOW(), NOW()),
  ('bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 'Watches', NOW(), NOW());
  
  
-- Inserts com GUIDs fixos na tabela colors
INSERT INTO colors (id, name, value) VALUES
  ('1ec9dcce-377c-4413-a5c0-038241295bdf', 'Vermelho',  '#FF0000'),
  ('92a89a53-010d-4c53-8bdd-db967dc09b25', 'Verde',      '#00FF00'),
  ('334f78f0-270c-433a-a7fd-269e734321da', 'Azul',       '#0000FF'),
  ('8c32b43b-5514-4440-9f23-d8395c53c2d8', 'Preto',      '#000000'),
  ('4447c101-3589-41eb-a409-9e0d73fec373', 'Branco',     '#FFFFFF'),
  ('1dc2533b-a152-4d94-a6a0-4427a41a251b', 'Amarelo',    '#FFFF00'),
  ('a5dff5b1-46b7-477f-b700-bbf9bc1e2468', 'Magenta',    '#FF00FF'),
  ('13d802f3-f9e3-4b3b-8f5a-67fd16f3433c', 'Ciano',      '#00FFFF'),
  ('75669645-0db1-461e-a242-e31f83510654', 'Laranja',    '#FFA500'),
  ('914d5a46-db2c-4b8e-81e8-a2f6a72b0e87', 'Roxo',       '#800080');



    -- Produto: Smartphone Galaxy X
INSERT INTO products (
    id, name, description, price, old_price, category_id, stock_quantity, sold,
    is_active, popular, created_at, updated_at
) VALUES (
    'a8ce040e-5b21-4010-86b2-6360434b8ca5',
    'Galaxy X 2025',
    'Smartphone de última geração com câmera 108MP, tela AMOLED e bateria de longa duração.',
    3999.90,
    4599.90,
    'a1cbd4e8-6725-4f59-ae50-427ecf5888b0',  -- Categoria: Smartphones
    150,
    10,
    TRUE,
    TRUE,
    NOW(),
    NOW()
);

-- Imagem do Produto
INSERT INTO product_images (
    id, product_id, url, created_at
) VALUES (
    '6e7f19b4-3d97-4f93-96b3-214a80fdc3d4',
    'a8ce040e-5b21-4010-86b2-6360434b8ca5',
    '/assets/products-images/product_1.png',
    NOW()
);

-- Cor associada: Preto
INSERT INTO product_colors (
    id, product_id, color_id, created_at, updated_at
) VALUES (
    '49c71c92-0454-4461-b306-edf0a9076404',
    'a8ce040e-5b21-4010-86b2-6360434b8ca5',
    '8c32b43b-5514-4440-9f23-d8395c53c2d8',  -- Preto
    NOW(),
    NOW()
);