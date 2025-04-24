CREATE EXTENSION IF NOT EXISTS "pgcrypto";

drop table customer_addresses;
drop table customers;
drop table categories;
ALTER TABLE product_images
DROP CONSTRAINT ck_product_image_url;




SELECT
	p.id AS product_id,
  p.name AS product_name,
  p.description,
  p.price,
  p.old_price,
  p.stock_quantity,
  p.sold,
  p.is_active,
  p.popular,
  p.created_at,
  p.updated_at,

  c.id AS category_id,
  c.name AS category_name,

  pi.id AS image_id,
  pi.url AS image_url,

  pc.id AS color_id,
  pc.color_value AS color
FROM products p
JOIN categories c ON p.category_id = c.id
LEFT JOIN product_images pi ON p.id = pi.product_id
LEFT JOIN product_colors pc ON p.id = pc.product_id
ORDER BY p.created_at DESC;




SELECT
  p.id,
  p.name,
  p.description,
  p.price,
  p.old_price,
  p.stock_quantity,
  p.sold,
  p.is_active,
  p.popular,
  p.created_at,
  p.updated_at,

  c.id AS category_id,
  c.name AS category_name,

  COALESCE(json_agg(DISTINCT pi.url) FILTER (WHERE pi.url IS NOT NULL), '[]') AS images,
  COALESCE(json_agg(DISTINCT pc.color_value) FILTER (WHERE pc.color_value IS NOT NULL), '[]') AS colors

FROM products p
JOIN categories c ON p.category_id = c.id
LEFT JOIN product_images pi ON p.id = pi.product_id
LEFT JOIN product_colors pc ON p.id = pc.product_id

GROUP BY
  p.id, p.name, p.description, p.price, p.old_price, p.stock_quantity,
  p.sold, p.is_active, p.popular, p.created_at, p.updated_at,
  c.id, c.name

ORDER BY p.created_at DESC;


SELECT 
    p.id AS product_id,
    p.name AS product_name,
    c.id AS category_id,
    c.name AS category_name
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE c.name IS NULL OR TRIM(c.name) = '';

SELECT id, name
FROM categories
WHERE TRIM(name) = '';

UPDATE categories
SET name = 'Sem Categoria'
WHERE TRIM(name) = '';






-- Recria permitindo caminhos locais também
ALTER TABLE product_images
ADD CONSTRAINT ck_product_image_url CHECK (
    url ~* '^(https?:\/\/.+\.(jpg|jpeg|png|webp|svg))|(^\/.+\.(jpg|jpeg|png|webp|svg))$'
);

CREATE TABLE customers (
id UUID PRIMARY KEY,
name VARCHAR(255) NOT NULL,
email VARCHAR(255) NOT NULL,
password VARCHAR(255) NOT NULL,
cpf CHAR(11),
deleted_at TIMESTAMP,
created_at TIMESTAMP NOT NULL,
updated_at TIMESTAMP NOT NULL,

-- 📛 CONSTRAINTS explícitas
CONSTRAINT uq_customers_email UNIQUE (email),
CONSTRAINT uq_customers_cpf UNIQUE (cpf)
);

CREATE TABLE customer_addresses (
id UUID PRIMARY KEY,
customer_id UUID NOT NULL,
street VARCHAR(255) NOT NULL,
number VARCHAR(50) NOT NULL,
city VARCHAR(100) NOT NULL,
state VARCHAR(50) NOT NULL,
zip_code VARCHAR(20) NOT NULL,
created_at TIMESTAMP NOT NULL,
updated_at TIMESTAMP NOT NULL DEFAULT now(), -- ✅ ADICIONE ESTA LINHA

CONSTRAINT fk_customer_addresses_customer
FOREIGN KEY (customer_id)
REFERENCES customers(id)
ON DELETE CASCADE
);

CREATE TABLE categories (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,

    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP NOT NULL,

    CONSTRAINT ck_categories_name_not_empty CHECK (char_length(name) > 0)
);





CREATE TABLE products (
    id UUID PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    description TEXT NOT NULL,
    price NUMERIC(18,2) NOT NULL,
    old_price NUMERIC(18,2),
    
    category_id UUID NOT NULL,
    stock_quantity INT NOT NULL,
    sold INT NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    popular BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP NOT NULL,
    deleted_at TIMESTAMP,

    -- 🔗 Chave estrangeira
    CONSTRAINT fk_products_category FOREIGN KEY (category_id)
        REFERENCES categories(id)
        ON DELETE RESTRICT,

    -- ✅ Constraints
    CONSTRAINT ck_product_name CHECK (char_length(name) BETWEEN 3 AND 150),
    CONSTRAINT ck_product_description CHECK (char_length(description) BETWEEN 10 AND 1000),
    CONSTRAINT ck_product_price CHECK (price > 0),
    CONSTRAINT ck_product_old_price CHECK (old_price IS NULL OR old_price >= 0),
    CONSTRAINT ck_product_stock CHECK (stock_quantity >= 0),
    CONSTRAINT ck_product_sold CHECK (sold >= 0),
    CONSTRAINT ck_product_dates CHECK (updated_at >= created_at)
);





CREATE TABLE product_images (
    id UUID PRIMARY KEY,
    product_id UUID NOT NULL,
    url TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL,

    -- 🔗 FK com ON DELETE CASCADE
    CONSTRAINT fk_product_images_product FOREIGN KEY (product_id)
        REFERENCES products(id)
        ON DELETE CASCADE,

    -- ✅ Validação de formato
    CONSTRAINT ck_product_image_url CHECK (
        url ~* '^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$'
    )
);




CREATE TABLE product_colors (
    id UUID PRIMARY KEY,
    product_id UUID NOT NULL,
    color_value VARCHAR(20) NOT NULL,
    created_at TIMESTAMP NOT NULL,

    CONSTRAINT fk_product_colors_product FOREIGN KEY (product_id)
        REFERENCES products(id)
        ON DELETE CASCADE,

    CONSTRAINT ck_product_color_not_empty CHECK (char_length(color_value) > 0),
    CONSTRAINT ck_product_color_format CHECK (
        color_value ~* '^#([0-9a-f]{3}|[0-9a-f]{6})$'
        OR color_value ~* '^[a-zA-Z]+$'
    )
);





-- INSERTS 


-- Categories com UUIDs fixos
INSERT INTO categories (id, name, created_at, updated_at) VALUES
  ('a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 'Smartphones', now(), now()),
  ('c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 'Cameras', now(), now()),
  ('abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 'Mobiles', now(), now()),
  ('b3d93042-4782-4c5d-b250-6018c43834f5', 'Speakers', now(), now()),
  ('d14a17fc-e499-4d55-988c-5e96e28a94f2', 'Mouse', now(), now()),
  ('bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 'Watches', now(), now());
  
  
  
  
-- Proudcts


-- Produto 1
INSERT INTO products (id, name, description, price, old_price, category_id, stock_quantity, sold, is_active, popular, created_at, updated_at)
VALUES ('d7cc8f3e-b9b0-4a3f-94ae-0170f02d1c0e', 'Bluetooth Headset Pro', 'Superior sound with noise cancellation.', 15.00, 20.00, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 100, 12, TRUE, FALSE, to_timestamp(1716634345), to_timestamp(1716634345));

INSERT INTO product_images (id, product_id, url, created_at) VALUES
('b7900d52-63fc-46ed-9688-5dced89ad7f3', 'd7cc8f3e-b9b0-4a3f-94ae-0170f02d1c0e', '/assets/products-images/product_1.png', now());

INSERT INTO product_colors (id, product_id, color_value, created_at) VALUES
('a7e2f4a3-2b5a-4786-b406-9357d6974d0e', 'd7cc8f3e-b9b0-4a3f-94ae-0170f02d1c0e', 'Black', now()),
('4b34cc4b-89ee-40d7-8ee5-bbd51fcb511c', 'd7cc8f3e-b9b0-4a3f-94ae-0170f02d1c0e', 'Red', now()),
('3c84df6f-6513-4d4b-8bb8-1eb4e63cdb8b', 'd7cc8f3e-b9b0-4a3f-94ae-0170f02d1c0e', 'White', now());

-- Produto 2

INSERT INTO products VALUES
('be198aa9-5601-402e-87f3-2974ec29dc08', 'Noise Cancelling Headphones', 'Premium wireless headset with crystal-clear audio.', 22.00, NULL, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 80, 20, TRUE, FALSE, to_timestamp(1716621345), to_timestamp(1716621345));

INSERT INTO product_images VALUES
('45f6cf65-9c17-4121-bc65-4dc0c417c1f5', 'be198aa9-5601-402e-87f3-2974ec29dc08', '/assets/products-images/product_2_1.png', now()),
('11f34a6c-6e99-40a5-b30f-3c8fdc00b1e3', 'be198aa9-5601-402e-87f3-2974ec29dc08', '/assets/products-images/product_2_2.png', now()),
('64d88b9b-e935-45b4-b2aa-0f1b3e7a4a3d', 'be198aa9-5601-402e-87f3-2974ec29dc08', '/assets/products-images/product_2_3.png', now()),
('ce798933-b938-45e2-83a3-054f1c0b7e4d', 'be198aa9-5601-402e-87f3-2974ec29dc08', '/assets/products-images/product_2_4.png', now());

INSERT INTO product_colors VALUES
('1c6d83f7-61b6-4055-8d6c-8ab2210e5f8a', 'be198aa9-5601-402e-87f3-2974ec29dc08', 'Black', now()),
('c0cfdb35-f0c2-463d-bb5e-74215bc6f4d9', 'be198aa9-5601-402e-87f3-2974ec29dc08', 'Red', now()),
('39947a6a-4b90-4bc5-8c5c-0e3bcd44a17c', 'be198aa9-5601-402e-87f3-2974ec29dc08', 'White', now()),
('cb7e1cd4-dc35-4d45-9a65-44d3c6cfe4c6', 'be198aa9-5601-402e-87f3-2974ec29dc08', 'Blue', now());

-- Produto 3

INSERT INTO products VALUES
('ca9640a0-3e59-4f58-8f47-2a12f8d07b36', 'Over-Ear Wireless Headphones', 'Comfortable and advanced sound for music lovers.', 20.00, 30.00, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 50, 18, TRUE, TRUE, to_timestamp(1716234545), to_timestamp(1716234545));

INSERT INTO product_images VALUES
('9ec94343-66e5-4dc6-934c-0cb9637cd197', 'ca9640a0-3e59-4f58-8f47-2a12f8d07b36', '/assets/products-images/product_3.png', now());

INSERT INTO product_colors VALUES
('fa2e0296-f498-41f5-a06a-f2f01d7a25aa', 'ca9640a0-3e59-4f58-8f47-2a12f8d07b36', 'Black', now()),
('fb5eb51f-d37c-4aa2-918e-68f97ae4bcf3', 'ca9640a0-3e59-4f58-8f47-2a12f8d07b36', 'White', now()),
('f529b91e-df90-4be4-a989-9df8f3678c88', 'ca9640a0-3e59-4f58-8f47-2a12f8d07b36', 'Blue', now());


-- Produto 4
INSERT INTO products VALUES
('f3a97c38-f10a-4602-84bb-5024411c4513', 'Wireless Noise Cancelling Headphones', 'Lightweight headphones for immersive listening.', 80.00, NULL, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 40, 10, TRUE, FALSE, to_timestamp(1716621345), to_timestamp(1716621345));

INSERT INTO product_images VALUES
('d44e1a65-b3f5-404b-8e7e-1c39f19bb376', 'f3a97c38-f10a-4602-84bb-5024411c4513', '/assets/products-images/product_4.png', now());

INSERT INTO product_colors VALUES
('5ebf6a7e-94e3-4dd3-8b9f-e2edc7e0a0a2', 'f3a97c38-f10a-4602-84bb-5024411c4513', 'Black', now()),
('ae2565c0-1378-4d4c-a8a2-6ddaa7beaeac', 'f3a97c38-f10a-4602-84bb-5024411c4513', 'Red', now()),
('e9a83ff3-63ce-4b0a-8ef0-84d547a5e98d', 'f3a97c38-f10a-4602-84bb-5024411c4513', 'Blue', now());

-- Produto 5

INSERT INTO products VALUES
('f0e51c1f-6a9d-4ad0-8ac5-5cc385a2ac33', 'Gaming Headphones with Mic', 'Immersive gaming headphones with built-in mic.', 40.00, NULL, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 30, 5, TRUE, FALSE, to_timestamp(1716622345), to_timestamp(1716622345));

INSERT INTO product_images VALUES
('ae4ae0c7-7a76-4a0f-8365-d87c22cb812a', 'f0e51c1f-6a9d-4ad0-8ac5-5cc385a2ac33', '/assets/products-images/product_5.png', now());

INSERT INTO product_colors VALUES
('87e394a4-073c-4b8e-8ae1-6e98191b28a2', 'f0e51c1f-6a9d-4ad0-8ac5-5cc385a2ac33', 'Red', now()),
('24e377f2-8399-41ed-a712-6d5ed12542f5', 'f0e51c1f-6a9d-4ad0-8ac5-5cc385a2ac33', 'White', now()),
('fc602d2e-2b6d-4cfd-bc31-01c530b116ed', 'f0e51c1f-6a9d-4ad0-8ac5-5cc385a2ac33', 'Blue', now());

-- produto 6

INSERT INTO products VALUES
('69439cfc-108f-46cf-9e1b-79cdd234b5f3', 'Sports Bluetooth Earphones', 'Sweat-resistant earphones for active users.', 60.00, 75.00, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 60, 9, TRUE, FALSE, to_timestamp(1716623345), to_timestamp(1716623345));

INSERT INTO product_images VALUES
('ed1e90df-2187-40ef-a262-b8b124989a55', '69439cfc-108f-46cf-9e1b-79cdd234b5f3', '/assets/products-images/product_6.png', now());

INSERT INTO product_colors VALUES
('5c6e7683-2c41-440b-b3a9-9cd1cc4f5f86', '69439cfc-108f-46cf-9e1b-79cdd234b5f3', 'XS', now()),
('e465b7c6-8bce-41d8-b1f6-12db95c83a62', '69439cfc-108f-46cf-9e1b-79cdd234b5f3', 'Black', now()),
('b99e5a4b-8406-4f95-a64f-c5dfd92f8c2e', '69439cfc-108f-46cf-9e1b-79cdd234b5f3', 'Red', now());

-- Produto 7
INSERT INTO products VALUES
('8c9c1e19-42c9-4d3f-a262-3612d21484ee', 'Foldable Wireless Headphones', 'Portable and comfortable headphones.', 20.00, NULL, 'a1cbd4e8-6725-4f59-ae50-427ecf5888b0', 70, 12, TRUE, FALSE, to_timestamp(1716624345), to_timestamp(1716624345));

INSERT INTO product_images VALUES
('de2513e4-58aa-4d49-b74b-fab12de8cf9a', '8c9c1e19-42c9-4d3f-a262-3612d21484ee', '/assets/products-images/product_7.png', now());

INSERT INTO product_colors VALUES
('7fdf531d-4ce7-41ae-9ac2-9b7b54dc66c3', '8c9c1e19-42c9-4d3f-a262-3612d21484ee', 'Black', now()),
('f2f9bcae-0600-4b37-a0fd-cd0be45eb2d9', '8c9c1e19-42c9-4d3f-a262-3612d21484ee', 'Red', now()),
('2be7f60e-1e13-4321-bb5d-1f0d2926a725', '8c9c1e19-42c9-4d3f-a262-3612d21484ee', 'White', now()),
('12f10be6-421e-49a5-93a3-8fbcfe127f1f', '8c9c1e19-42c9-4d3f-a262-3612d21484ee', 'Blue', now());

-- produto 8
INSERT INTO products VALUES
('54e59f7f-8c9e-469e-b9bc-5dc3fd52d9f7', '4K DSLR Camera', 'High-res DSLR camera for pros.', 20.00, NULL, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 90, 22, TRUE, FALSE, to_timestamp(1716626345), to_timestamp(1716626345));

INSERT INTO product_images VALUES
('15b0417b-80ac-45b2-9bc9-bbd5bdb2824d', '54e59f7f-8c9e-469e-b9bc-5dc3fd52d9f7', '/assets/products-images/product_9.png', now());

INSERT INTO product_colors VALUES
('1082c7e1-bdc7-4097-9338-8e92e7f2c5aa', '54e59f7f-8c9e-469e-b9bc-5dc3fd52d9f7', 'Black', now()),
('3c163d06-d354-4a08-9d5f-3a8b00ae30b4', '54e59f7f-8c9e-469e-b9bc-5dc3fd52d9f7', 'Red', now());

-- Produto 10
INSERT INTO products VALUES
('40bd76f1-63e6-44bb-b7b5-d3e8ea30a6bb', 'Compact Digital Camera', 'Lightweight and easy to use.', 20.00, NULL, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 35, 14, TRUE, FALSE, to_timestamp(1716627345), to_timestamp(1716627345));

INSERT INTO product_images VALUES
('af3a0cc2-690b-42f7-a73a-8479a328d4c3', '40bd76f1-63e6-44bb-b7b5-d3e8ea30a6bb', '/assets/products-images/product_10.png', now());

INSERT INTO product_colors VALUES
('bfcdbcd5-c5e4-4bcd-b3ce-b5bc74a195e6', '40bd76f1-63e6-44bb-b7b5-d3e8ea30a6bb', 'Black', now()),
('a429a6c7-4e06-4dcf-8902-1c3fd4a1e6f1', '40bd76f1-63e6-44bb-b7b5-d3e8ea30a6bb', 'Red', now());


--produto 11
INSERT INTO products VALUES
('4b7d2d6e-11e4-4d5a-9be0-02dfc33aaf3c', 'Outdoor Action Camera', 'Waterproof action camera built for adventurers.', 30.00, NULL, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 60, 11, TRUE, FALSE, to_timestamp(1716628345), to_timestamp(1716628345));

INSERT INTO product_images VALUES
('993f4b44-808d-44b5-9133-b5866c175f35', '4b7d2d6e-11e4-4d5a-9be0-02dfc33aaf3c', '/assets/products-images/product_11.png', now());

INSERT INTO product_colors VALUES
('a7e6fd58-38f5-4f17-88a6-57df9cce83f1', '4b7d2d6e-11e4-4d5a-9be0-02dfc33aaf3c', 'Red', now());

--produto 12
INSERT INTO products VALUES
('33a1eb8c-cf5d-4b39-aec3-8a3e3a5791a7', 'Professional Mirrorless Camera', '4K mirrorless camera with stabilization.', 10.00, 19.00, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 50, 33, TRUE, TRUE, to_timestamp(1716629345), to_timestamp(1716629345));

INSERT INTO product_images VALUES
('d87d6d6f-5904-4ccf-b860-e7cf270c0203', '33a1eb8c-cf5d-4b39-aec3-8a3e3a5791a7', '/assets/products-images/product_12.png', now());

INSERT INTO product_colors VALUES
('b3be81e5-5744-4d2f-a0d3-64c9c57a4f3c', '33a1eb8c-cf5d-4b39-aec3-8a3e3a5791a7', 'Black', now()),
('f615af3d-8df5-4c3d-9135-768ce0fdc709', '33a1eb8c-cf5d-4b39-aec3-8a3e3a5791a7', 'Red', now());

--produto 13
INSERT INTO products VALUES
('f7de98de-245f-4b59-8458-112e8604070f', 'Camera Lens Kit', 'Versatile lens kit for professionals.', 20.00, NULL, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 75, 12, TRUE, FALSE, to_timestamp(1716630345), to_timestamp(1716630345));

INSERT INTO product_images VALUES
('12790c40-c95d-4bb6-98d1-56f5a9df11db', 'f7de98de-245f-4b59-8458-112e8604070f', '/assets/products-images/product_13.png', now());

INSERT INTO product_colors VALUES
('d2819a76-9251-4406-8039-50834662b8b4', 'f7de98de-245f-4b59-8458-112e8604070f', 'Black', now()),
('8cdb2cb8-6b7f-4f48-b0df-f63eb1cd5d3e', 'f7de98de-245f-4b59-8458-112e8604070f', 'Red', now());

--produto 14
INSERT INTO products VALUES
('ba5b29d0-0636-4bb6-9872-cf5c0c46d041', 'Camera Tripod Stand', 'Stable tripod stand for indoor/outdoor shots.', 20.00, NULL, 'c7f8e3e9-c5cd-4a80-b0db-ec2dbe2dfab3', 90, 9, TRUE, FALSE, to_timestamp(1716631345), to_timestamp(1716631345));

INSERT INTO product_images VALUES
('94938f8e-d2e6-41a4-bf12-40bcd3cc00f9', 'ba5b29d0-0636-4bb6-9872-cf5c0c46d041', '/assets/products-images/product_14.png', now());

INSERT INTO product_colors VALUES
('84d54e8d-b963-4711-a43b-4a9d1ecaf648', 'ba5b29d0-0636-4bb6-9872-cf5c0c46d041', 'Black', now()),
('f2ef04b0-8ac4-49c3-9db9-7fc947dbcc5b', 'ba5b29d0-0636-4bb6-9872-cf5c0c46d041', 'Red', now());


--produto 15
INSERT INTO products VALUES
('5e3e96c2-5096-47c0-8a9e-2dc11b8cfe87', 'Camera Flash Light', 'Bright and powerful flash light for photography.', 15.00, NULL, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 40, 17, TRUE, TRUE, to_timestamp(1716632345), to_timestamp(1716632345));

INSERT INTO product_images VALUES
('97e86515-7f49-4fd5-8b10-2643dd1e88e5', '5e3e96c2-5096-47c0-8a9e-2dc11b8cfe87', '/assets/products-images/product_15.png', now());

INSERT INTO product_colors VALUES
('11df8dbb-b1f5-43f5-a8df-fd388d2d6f9f', '5e3e96c2-5096-47c0-8a9e-2dc11b8cfe87', 'XS', now()),
('ffc10fdf-8d42-4d6a-9e30-f1a4104db94c', '5e3e96c2-5096-47c0-8a9e-2dc11b8cfe87', 'Black', now()),
('2ae4715f-5b8c-4c43-8c35-2a1c95b3a9d6', '5e3e96c2-5096-47c0-8a9e-2dc11b8cfe87', 'Red', now());


--produto 16
INSERT INTO products VALUES
('ed90a8b1-d5b5-47de-84cb-705f3df8e51a', '5G Tecno Mobile', 'Durable 5G phone with excellent storage.', 20.00, 28.00, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 120, 31, TRUE, FALSE, to_timestamp(1716633345), to_timestamp(1716633345));

INSERT INTO product_images VALUES
('25d40b8d-2fc3-4ff1-9867-3f156e8e38ef', 'ed90a8b1-d5b5-47de-84cb-705f3df8e51a', '/assets/products-images/product_16.png', now());

INSERT INTO product_colors VALUES
('a24c0907-e85b-4e6c-8cf1-b17134e9f623', 'ed90a8b1-d5b5-47de-84cb-705f3df8e51a', 'Black', now()),
('3d270de7-7428-48ec-81fc-f7d80ef204ce', 'ed90a8b1-d5b5-47de-84cb-705f3df8e51a', 'Red', now()),
('e9dbe34c-fb0e-49f1-94cf-7341870b5ed4', 'ed90a8b1-d5b5-47de-84cb-705f3df8e51a', 'White', now());

--produto 17
INSERT INTO products VALUES
('4c64ab4f-040f-421e-9f8a-d6119646c12d', 'Smartphone Camera Lens Kit', 'Enhance mobile photography with lens kit.', 30.00, NULL, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 60, 23, TRUE, FALSE, to_timestamp(1716634345), to_timestamp(1716634345));

INSERT INTO product_images VALUES
('7b6c7ad7-0ec9-4c85-91f2-2fbc4473aa4b', '4c64ab4f-040f-421e-9f8a-d6119646c12d', '/assets/products-images/product_17.png', now());

INSERT INTO product_colors VALUES
('e1a1a7e2-5ec2-439c-82a1-4a27d5b3735e', '4c64ab4f-040f-421e-9f8a-d6119646c12d', 'Black', now()),
('80d27f0c-f9b5-4b20-b54b-31991a9be80b', '4c64ab4f-040f-421e-9f8a-d6119646c12d', 'Red', now()),
('5010ac2b-6cb6-419f-a0df-326cb10c59db', '4c64ab4f-040f-421e-9f8a-d6119646c12d', 'White', now()),
('96e962cc-d4ff-47a0-89d3-7595d65861ff', '4c64ab4f-040f-421e-9f8a-d6119646c12d', 'Blue', now());

--produto 18
INSERT INTO products VALUES
('8c160d4d-0122-4354-84a0-6c3058e23541', 'Mobile Phone 4G', 'Fast and efficient mobile phone.', 10.00, NULL, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 100, 12, TRUE, FALSE, to_timestamp(1716635345), to_timestamp(1716635345));

INSERT INTO product_images VALUES
('7c2e9f39-3dc8-43d5-90d7-0a85cd57a6b9', '8c160d4d-0122-4354-84a0-6c3058e23541', '/assets/products-images/product_18.png', now());

INSERT INTO product_colors VALUES
('2b6b3e9f-81e5-4fa4-bff0-95e622847da9', '8c160d4d-0122-4354-84a0-6c3058e23541', 'Black', now()),
('e6e2df0a-e784-4b9e-bbc1-52eaa11b67ce', '8c160d4d-0122-4354-84a0-6c3058e23541', 'Red', now()),
('18a59df2-d19a-4e4b-bc9e-e3897b9cc6d1', '8c160d4d-0122-4354-84a0-6c3058e23541', 'White', now()),
('f6d8f5c2-c478-43f5-9444-3e7a5b60156e', '8c160d4d-0122-4354-84a0-6c3058e23541', 'Blue', now());


--produto 19
INSERT INTO products VALUES
('30966f4b-5ae1-4064-9323-f3fdb67c3f31', '5G Smartphone', 'Ultra-fast smartphone with vibrant display.', 30.00, NULL, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 90, 19, TRUE, FALSE, to_timestamp(1716636345), to_timestamp(1716636345));

INSERT INTO product_images VALUES
('cf53f6f6-1ea7-44a6-8eaf-f5f13ad89f1f', '30966f4b-5ae1-4064-9323-f3fdb67c3f31', '/assets/products-images/product_19.png', now());

INSERT INTO product_colors VALUES
('b9e35289-1f82-49c2-a0d7-b8be80e0b2e7', '30966f4b-5ae1-4064-9323-f3fdb67c3f31', 'Black', now()),
('89c6b58a-3bd1-48ec-84d5-7a0e4b8dfe69', '30966f4b-5ae1-4064-9323-f3fdb67c3f31', 'Red', now()),
('1acbf03d-8b74-48ae-9469-31cc1c51ec41', '30966f4b-5ae1-4064-9323-f3fdb67c3f31', 'White', now());

--produto 20
INSERT INTO products VALUES
('2a3a72b7-9e2b-45ec-84b0-3f1650bdb6f1', 'Mobile Phone Case', 'Shock-resistant premium phone case.', 20.00, 25.00, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 70, 27, TRUE, FALSE, to_timestamp(1716637345), to_timestamp(1716637345));

INSERT INTO product_images VALUES
('f13f1474-5107-4b1e-b68d-6b8c0d84f469', '2a3a72b7-9e2b-45ec-84b0-3f1650bdb6f1', '/assets/products-images/product_20.png', now());

INSERT INTO product_colors VALUES
('71c152c9-79e2-4cc4-b7e7-25f81eeb57ee', '2a3a72b7-9e2b-45ec-84b0-3f1650bdb6f1', 'Black', now()),
('d8ab2d11-364e-4df7-885b-d4f507fc438b', '2a3a72b7-9e2b-45ec-84b0-3f1650bdb6f1', 'Red', now()),
('9a96f59a-8028-4ebc-b0d9-c929342e55e9', '2a3a72b7-9e2b-45ec-84b0-3f1650bdb6f1', 'White', now());

--produto 21
INSERT INTO products VALUES
('3bb31983-362d-4f7d-b370-dc37c3f2e3d0', 'Mobile Charger', 'High-speed mobile charging cable and adapter.', 30.00, NULL, 'abf7a949-e2fd-42ec-9a08-d4f3e5b0426d', 120, 33, TRUE, FALSE, to_timestamp(1716638345), to_timestamp(1716638345));

INSERT INTO product_images VALUES
('f7091a20-5d95-4c9d-b93c-0e4e1e56aeb9', '3bb31983-362d-4f7d-b370-dc37c3f2e3d0', '/assets/products-images/product_21.png', now());

INSERT INTO product_colors VALUES
('ac3cf3c6-12c8-4d18-99c7-8038f77e2f30', '3bb31983-362d-4f7d-b370-dc37c3f2e3d0', 'Black', now()),
('2896e938-f7e6-4a61-9c60-70d2c676b3cf', '3bb31983-362d-4f7d-b370-dc37c3f2e3d0', 'Red', now()),
('6d876172-273f-42f2-a13a-5ac40e7a9a29', '3bb31983-362d-4f7d-b370-dc37c3f2e3d0', 'White', now()),
('c52b2e34-b7fc-4ac3-b891-8f4268f50e8b', '3bb31983-362d-4f7d-b370-dc37c3f2e3d0', 'Blue', now());

--produto 22
INSERT INTO products VALUES
('5d4d4796-6b99-4f49-bc27-2e2d46f9c66e', 'Smartwatch Phone', 'Smartwatch with phone connectivity & notifications.', 400.00, 460.00, 'b3d93042-4782-4c5d-b250-6018c43834f5', 35, 8, TRUE, TRUE, to_timestamp(1716639345), to_timestamp(1716639345));

INSERT INTO product_images VALUES
('33a1b9e9-26a3-4382-b4d0-92a008f50c89', '5d4d4796-6b99-4f49-bc27-2e2d46f9c66e', '/assets/products-images/product_22.png', now());

INSERT INTO product_colors VALUES
('0bff2937-cbe5-4b26-9274-3a9e9fa5a6cb', '5d4d4796-6b99-4f49-bc27-2e2d46f9c66e', 'Red', now()),
('bbf75e46-1164-4037-abe0-643073377d52', '5d4d4796-6b99-4f49-bc27-2e2d46f9c66e', 'White', now()),
('ae450186-76f3-4fe7-b914-3e4d9e60456e', '5d4d4796-6b99-4f49-bc27-2e2d46f9c66e', 'Blue', now());

--produto 23
INSERT INTO products VALUES
('87118d67-b9fa-4fd6-9bb2-b6ebd96333da', 'Bluetooth Mobile Speaker', 'Compact speaker with rich Bluetooth sound.', 190.00, NULL, 'b3d93042-4782-4c5d-b250-6018c43834f5', 70, 22, TRUE, FALSE, to_timestamp(1716640345), to_timestamp(1716640345));

INSERT INTO product_images VALUES
('e881fa12-b9a2-4475-b1ef-0993a949b7dc', '87118d67-b9fa-4fd6-9bb2-b6ebd96333da', '/assets/products-images/product_23.png', now());

INSERT INTO product_colors VALUES
('31f73090-9ff1-4747-a25f-6219c4bb5a8b', '87118d67-b9fa-4fd6-9bb2-b6ebd96333da', 'Black', now()),
('314cc3fc-fd87-4359-8572-2ed4ec3c99c4', '87118d67-b9fa-4fd6-9bb2-b6ebd96333da', 'Red', now()),
('f1549e2e-50f4-4e9c-bd58-5fbc00f74bb8', '87118d67-b9fa-4fd6-9bb2-b6ebd96333da', 'White', now());

--produto 24
INSERT INTO products VALUES
('df26e155-f94f-4efb-89e1-fbce1e52476a', 'Portable Bluetooth Speaker', 'Deep bass speaker for parties and events.', 250.00, 280.00, 'b3d93042-4782-4c5d-b250-6018c43834f5', 55, 29, TRUE, FALSE, to_timestamp(1716641345), to_timestamp(1716641345));

INSERT INTO product_images VALUES
('f1600fa6-b68b-41de-b86f-f5e02998aaad', 'df26e155-f94f-4efb-89e1-fbce1e52476a', '/assets/products-images/product_24.png', now());

INSERT INTO product_colors VALUES
('0c1f3e2e-5411-4eb0-b6ee-101f3348b230', 'df26e155-f94f-4efb-89e1-fbce1e52476a', 'Black', now()),
('5ea94e70-79d7-4f56-bf1d-9bc9cdb94952', 'df26e155-f94f-4efb-89e1-fbce1e52476a', 'Red', now()),
('a6d82da0-f3ea-4fc3-9955-4ff6ad4d546f', 'df26e155-f94f-4efb-89e1-fbce1e52476a', 'White', now());


--produto 25
INSERT INTO products VALUES
('cdb9639f-42d0-4d02-8ad2-801f58532f4b', 'Smart Bluetooth Speaker', 'Voice recognition smart speaker.', 20.00, NULL, 'b3d93042-4782-4c5d-b250-6018c43834f5', 80, 16, TRUE, FALSE, to_timestamp(1716642345), to_timestamp(1716642345));

INSERT INTO product_images VALUES
('22f75e1d-06f4-4020-8711-5093f122eb67', 'cdb9639f-42d0-4d02-8ad2-801f58532f4b', '/assets/products-images/product_25.png', now());

INSERT INTO product_colors VALUES
('fba847aa-f6e4-4018-a3b4-e899b2b51f44', 'cdb9639f-42d0-4d02-8ad2-801f58532f4b', 'Red', now()),
('27d89bd7-2ef5-466e-8f69-3dd03ec0597f', 'cdb9639f-42d0-4d02-8ad2-801f58532f4b', 'White', now()),
('21db054f-4e82-4b64-9318-18b0801cdd2b', 'cdb9639f-42d0-4d02-8ad2-801f58532f4b', 'Blue', now());

--produto 26
INSERT INTO products VALUES
('dcf19546-e3b1-4dbe-a9bb-355bce0a7199', 'Portable Mini Bluetooth Speaker', 'Mini speaker with powerful on-the-go sound.', 22.00, NULL, 'b3d93042-4782-4c5d-b250-6018c43834f5', 90, 13, TRUE, FALSE, to_timestamp(1716643345), to_timestamp(1716643345));

INSERT INTO product_images VALUES
('813cf531-3b89-4395-8b71-7ddc5f124c6a', 'dcf19546-e3b1-4dbe-a9bb-355bce0a7199', '/assets/products-images/product_26.png', now());

INSERT INTO product_colors VALUES
('c8cf3fd4-3d24-4ae8-a720-47cfc7d7b338', 'dcf19546-e3b1-4dbe-a9bb-355bce0a7199', 'Black', now()),
('06faebce-6eb5-4fc5-b97e-82a9e31cf6e2', 'dcf19546-e3b1-4dbe-a9bb-355bce0a7199', 'Red', now()),
('c6c75eb7-abe4-42a6-90a4-fb69e0b73827', 'dcf19546-e3b1-4dbe-a9bb-355bce0a7199', 'White', now());

--produto 27
INSERT INTO products VALUES
('4f0039fc-2b3c-49c1-8e8f-3bcf2764cdb4', 'Wireless Home Speaker', 'Home theater-quality wireless speaker.', 30.00, NULL, 'b3d93042-4782-4c5d-b250-6018c43834f5', 40, 18, TRUE, TRUE, to_timestamp(1716644345), to_timestamp(1716644345));

INSERT INTO product_images VALUES
('d9095b62-0258-4039-9911-89e1e50f5c3b', '4f0039fc-2b3c-49c1-8e8f-3bcf2764cdb4', '/assets/products-images/product_27.png', now());

INSERT INTO product_colors VALUES
('06fa15ef-2f2e-4200-9957-471d6cf89352', '4f0039fc-2b3c-49c1-8e8f-3bcf2764cdb4', 'Black', now()),
('cf3c4c82-c2a3-4e8f-a64d-bf2939e6b4d7', '4f0039fc-2b3c-49c1-8e8f-3bcf2764cdb4', 'White', now());

--produto 28
INSERT INTO products VALUES
('8c4cbd09-9720-4e50-a29f-08c4d3cc02e2', 'Surround Sound Speaker', 'Cinematic surround sound experience.', 530.00, 600.00, 'b3d93042-4782-4c5d-b250-6018c43834f5', 25, 14, TRUE, FALSE, to_timestamp(1716645345), to_timestamp(1716645345));

INSERT INTO product_images VALUES
('00cd3540-9a30-4a00-a72d-3cb8ee70cd5f', '8c4cbd09-9720-4e50-a29f-08c4d3cc02e2', '/assets/products-images/product_28.png', now());

INSERT INTO product_colors VALUES
('865dd5a1-b3d8-4c0a-9d4b-5f8e929be530', '8c4cbd09-9720-4e50-a29f-08c4d3cc02e2', 'Black', now()),
('a51c4d85-caf6-4039-bcd2-896ed25f9a6e', '8c4cbd09-9720-4e50-a29f-08c4d3cc02e2', 'Red', now()),
('849dcd02-91cf-4a11-8c4b-c0a5d1e64cf4', '8c4cbd09-9720-4e50-a29f-08c4d3cc02e2', 'White', now());


--produto 29
INSERT INTO products VALUES
('4fba12de-d0ff-4307-b7e3-0c314db00cf7', 'Wireless Gaming Mouse', 'Ergonomic mouse with low-latency.', 120.00, NULL, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 100, 40, TRUE, TRUE, to_timestamp(1716646345), to_timestamp(1716646345));

INSERT INTO product_images VALUES
('0e7484d7-3282-4a99-9827-2fc5e25fa9c1', '4fba12de-d0ff-4307-b7e3-0c314db00cf7', '/assets/products-images/product_29.png', now());

INSERT INTO product_colors VALUES
('6be3e4a1-1bd6-45ff-982f-bec0aa1e2bb4', '4fba12de-d0ff-4307-b7e3-0c314db00cf7', 'Black', now()),
('a348b06c-d546-4bb4-b84a-f6dd80ec3748', '4fba12de-d0ff-4307-b7e3-0c314db00cf7', 'Red', now()),
('4adf6c8b-b771-4323-b9cb-2fc087d9bdbc', '4fba12de-d0ff-4307-b7e3-0c314db00cf7', 'White', now()),
('e963b192-c11d-4df5-930e-25a7e82fd3b3', '4fba12de-d0ff-4307-b7e3-0c314db00cf7', 'Blue', now());

--produto 30
INSERT INTO products VALUES
('cc3f8650-b7d4-4cb2-a6b0-9e574f3f4c43', 'Ergonomic Wireless Mouse', 'Comfortable design for extended use.', 90.00, 110.00, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 85, 31, TRUE, FALSE, to_timestamp(1716647345), to_timestamp(1716647345));

INSERT INTO product_images VALUES
('b53f0c27-7a13-439e-8bff-9fc61206b7ef', 'cc3f8650-b7d4-4cb2-a6b0-9e574f3f4c43', '/assets/products-images/product_30.png', now());

INSERT INTO product_colors VALUES
('8fddc174-09a9-47a6-a535-3f6b0ab11b20', 'cc3f8650-b7d4-4cb2-a6b0-9e574f3f4c43', 'Black', now()),
('ac7b8adf-02db-4aa3-a38f-5df121e49f0b', 'cc3f8650-b7d4-4cb2-a6b0-9e574f3f4c43', 'Red', now()),
('5dcfd38f-e3f5-4296-833a-2b145c57a00f', 'cc3f8650-b7d4-4cb2-a6b0-9e574f3f4c43', 'Blue', now());

--produto 31
INSERT INTO products VALUES
('2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', 'RGB Gaming Mouse', 'Customizable mouse with RGB and sensors.', 40.00, 50.00, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 110, 25, TRUE, TRUE, to_timestamp(1716648345), to_timestamp(1716648345));

INSERT INTO product_images VALUES
('f28eb742-fc31-4b0e-8235-3f1780ed1c0f', '2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', '/assets/products-images/product_31.png', now());

INSERT INTO product_colors VALUES
('75db0227-e0b1-49b8-9331-512ea6486bd1', '2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', 'Black', now()),
('3c42edbe-39db-4a41-8e6f-e1498c8f5e69', '2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', 'Red', now()),
('e030cb9b-dfbd-4fd5-b143-c69a36edc6c2', '2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', 'White', now()),
('115f79fc-ec1e-43e8-8009-d17b37d44c25', '2cf6ff4f-58e4-4714-9564-f92fe9c83d3f', 'Blue', now());

--produto 32
INSERT INTO products VALUES
('f450b5f6-d861-41b8-87d7-c0bc577c79d1', 'Wireless Mouse with USB Receiver', 'Reliable wireless mouse for everyday use.', 40.00, NULL, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 60, 13, TRUE, FALSE, to_timestamp(1716649345), to_timestamp(1716649345));

INSERT INTO product_images VALUES
('f90f6c4f-70d1-43ea-8dc0-4059f9e229eb', 'f450b5f6-d861-41b8-87d7-c0bc577c79d1', '/assets/products-images/product_32.png', now());

INSERT INTO product_colors VALUES
('88f1a282-bf91-4ae3-8c3e-192d08b31a6c', 'f450b5f6-d861-41b8-87d7-c0bc577c79d1', 'Black', now()),
('11f22763-18b2-4417-87ab-8a13ad2c71c3', 'f450b5f6-d861-41b8-87d7-c0bc577c79d1', 'Red', now()),
('6ebd6764-2bdf-4b3d-835c-0247b812f0a3', 'f450b5f6-d861-41b8-87d7-c0bc577c79d1', 'White', now());

--produto 33
INSERT INTO products VALUES
('3d5d5b71-447f-4237-841f-2d37941c0591', 'Bluetooth Multi-Device Mouse', 'Switch between devices easily via Bluetooth.', 80.00, NULL, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 90, 17, TRUE, FALSE, to_timestamp(1716650345), to_timestamp(1716650345));

INSERT INTO product_images VALUES
('47e7cf3f-3173-41cb-9a8c-78a0b93e0d64', '3d5d5b71-447f-4237-841f-2d37941c0591', '/assets/products-images/product_33.png', now());

INSERT INTO product_colors VALUES
('8d60d244-6ab7-44c5-a567-e05446c2b7c7', '3d5d5b71-447f-4237-841f-2d37941c0591', 'Black', now()),
('bfd8d3f9-b4c9-4228-b029-8d79bcbe0a27', '3d5d5b71-447f-4237-841f-2d37941c0591', 'Red', now()),
('7fd94bff-2b0a-4a98-b9fa-d82d43c4802e', '3d5d5b71-447f-4237-841f-2d37941c0591', 'White', now()),
('16672008-9319-4a6c-8452-3b80066e42cb', '3d5d5b71-447f-4237-841f-2d37941c0591', 'Blue', now());

--produto 34
INSERT INTO products VALUES
('177d84a2-60d8-4e70-b3b1-b9c33dfe37b0', 'Compact Wireless Mouse', 'Portable and travel-friendly mouse.', 30.00, NULL, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 100, 20, TRUE, FALSE, to_timestamp(1716651345), to_timestamp(1716651345));

INSERT INTO product_images VALUES
('a144c2f3-01d5-4e3e-90b0-6e0308e2083f', '177d84a2-60d8-4e70-b3b1-b9c33dfe37b0', '/assets/products-images/product_34.png', now());

INSERT INTO product_colors VALUES
('fc7f08ed-ff7f-4199-8468-1b65f0242f9f', '177d84a2-60d8-4e70-b3b1-b9c33dfe37b0', 'Black', now()),
('119214c4-00be-4d30-988c-e886f02b8c1b', '177d84a2-60d8-4e70-b3b1-b9c33dfe37b0', 'Red', now()),
('e5f62167-5ee7-43df-b370-4fc9d1df81aa', '177d84a2-60d8-4e70-b3b1-b9c33dfe37b0', 'Blue', now());

--produto 35
INSERT INTO products VALUES
('f6fbaaca-fac1-42ae-90ae-0df2b2df0600', 'Gaming Mouse with Customizable Weights', 'Performance mouse with adjustable balance.', 15.00, NULL, 'd14a17fc-e499-4d55-988c-5e96e28a94f2', 75, 19, TRUE, TRUE, to_timestamp(1716652345), to_timestamp(1716652345));

INSERT INTO product_images VALUES
('0ec3791a-41c7-426b-8352-d6209d7ff12b', 'f6fbaaca-fac1-42ae-90ae-0df2b2df0600', '/assets/products-images/product_35.png', now());

INSERT INTO product_colors VALUES
('3498f798-daf9-4e4f-b1f5-021be58ec373', 'f6fbaaca-fac1-42ae-90ae-0df2b2df0600', 'Black', now()),
('2e3ebf3a-30c0-4a96-b8e3-9c05808ff7a9', 'f6fbaaca-fac1-42ae-90ae-0df2b2df0600', 'Red', now()),
('f3d6e476-fbcd-49a8-94ec-493ddfb42e2c', 'f6fbaaca-fac1-42ae-90ae-0df2b2df0600', 'White', now()),
('b4c3a46a-1734-4302-b9bc-09bcbe2e9c30', 'f6fbaaca-fac1-42ae-90ae-0df2b2df0600', 'Blue', now());

--produto 36
INSERT INTO products VALUES
('f6450fd4-87d2-47d2-a55d-19c1616b4a15', 'Smart Fitness Watch', 'All-in-one watch for fitness tracking.', 20.00, 28.00, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 60, 27, TRUE, TRUE, to_timestamp(1716653345), to_timestamp(1716653345));

INSERT INTO product_images VALUES
('17344a64-9abf-4b3f-95c4-e91dd9f3f7d5', 'f6450fd4-87d2-47d2-a55d-19c1616b4a15', '/assets/products-images/product_36.png', now());

INSERT INTO product_colors VALUES
('c19e28b0-0a79-48ef-9b18-9fae1c705e83', 'f6450fd4-87d2-47d2-a55d-19c1616b4a15', 'Black', now()),
('18c53b83-271c-4ec4-91e0-e32b90f62230', 'f6450fd4-87d2-47d2-a55d-19c1616b4a15', 'Red', now()),
('e973c9f6-b76a-419c-9a3e-e82a8b80e5bb', 'f6450fd4-87d2-47d2-a55d-19c1616b4a15', 'White', now()),
('358a2a33-5f10-4c7c-bf49-72ce1dc8f0e1', 'f6450fd4-87d2-47d2-a55d-19c1616b4a15', 'Blue', now());

--produto 37
INSERT INTO products VALUES
('af38a623-45bb-4191-a73e-9256246de3ef', 'Luxury Smartwatch', 'Elegant smartwatch with premium features.', 450.00, NULL, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 30, 10, TRUE, FALSE, to_timestamp(1716654345), to_timestamp(1716654345));

INSERT INTO product_images VALUES
('fcf1572e-2df1-4dd2-9c61-6f2e4913272f', 'af38a623-45bb-4191-a73e-9256246de3ef', '/assets/products-images/product_37.png', now());

INSERT INTO product_colors VALUES
('3467fbbc-b45b-4e3b-92a6-b1cbbf7b98bc', 'af38a623-45bb-4191-a73e-9256246de3ef', 'Gold', now()),
('62b49310-2b1c-4fd7-8492-708d47827a1e', 'af38a623-45bb-4191-a73e-9256246de3ef', 'Silver', now()),
('0ef39583-396f-4fe9-a08a-f81313d118c5', 'af38a623-45bb-4191-a73e-9256246de3ef', 'Black', now());

--produto 38
INSERT INTO products VALUES
('2c1ef152-0b88-487b-899e-bb8fc9c87c8f', 'Sports Smartwatch', 'Rugged smartwatch with health tracking.', 270.00, NULL, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 42, 21, TRUE, FALSE, to_timestamp(1716655345), to_timestamp(1716655345));

INSERT INTO product_images VALUES
('5f87951c-dc92-4631-9df8-cce582847dee', '2c1ef152-0b88-487b-899e-bb8fc9c87c8f', '/assets/products-images/product_38.png', now());

INSERT INTO product_colors VALUES
('e3d3fd2f-0ebc-419f-b2b3-07fc0d1b69b4', '2c1ef152-0b88-487b-899e-bb8fc9c87c8f', 'Red', now()),
('f99736ae-e3f6-495b-8a56-d5c059cc96b2', '2c1ef152-0b88-487b-899e-bb8fc9c87c8f', 'Blue', now()),
('2d8cf9b3-76d2-4f62-9fce-0281f38dc256', '2c1ef152-0b88-487b-899e-bb8fc9c87c8f', 'Black', now());

--produto 39
INSERT INTO products VALUES
('0375e83a-d2b0-4ec3-8595-cd35a9a98613', 'Android Smartwatch', 'Smartwatch with seamless Android sync.', 20.00, NULL, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 88, 14, TRUE, FALSE, to_timestamp(1716656345), to_timestamp(1716656345));

INSERT INTO product_images VALUES
('c4893bd6-2964-426e-9c47-091c0d9bc8c3', '0375e83a-d2b0-4ec3-8595-cd35a9a98613', '/assets/products-images/product_39.png', now());

INSERT INTO product_colors VALUES
('36e7ed55-5749-44e1-84a2-3edc86a070e6', '0375e83a-d2b0-4ec3-8595-cd35a9a98613', 'Black', now()),
('f12e2454-4d94-41c6-a0a4-5fa4561297e2', '0375e83a-d2b0-4ec3-8595-cd35a9a98613', 'Red', now()),
('c13752be-0032-4ee0-b51b-0b1a9c0d84df', '0375e83a-d2b0-4ec3-8595-cd35a9a98613', 'White', now()),
('98282318-95ed-49ed-82ae-ff2d4609b7a2', '0375e83a-d2b0-4ec3-8595-cd35a9a98613', 'Blue', now());

--produto 40
INSERT INTO products VALUES
('518376f4-f404-4b21-b370-b9e77c8ee9e0', 'Round Dial Smartwatch', 'Stylish round dial, full touchscreen.', 350.00, NULL, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 33, 19, TRUE, FALSE, to_timestamp(1716657345), to_timestamp(1716657345));

INSERT INTO product_images VALUES
('b61e26f4-9b10-423f-9330-87921817830a', '518376f4-f404-4b21-b370-b9e77c8ee9e0', '/assets/products-images/product_40.png', now());

INSERT INTO product_colors VALUES
('f2f02e94-75e7-426f-8aef-81f7b9b6d153', '518376f4-f404-4b21-b370-b9e77c8ee9e0', 'Gold', now()),
('bbcf98d7-0cbf-4b46-b0b7-e91b1273a51c', '518376f4-f404-4b21-b370-b9e77c8ee9e0', 'Silver', now()),
('85cbff3f-d117-4fc6-a8ff-827c37c6a1ea', '518376f4-f404-4b21-b370-b9e77c8ee9e0', 'Black', now()),
('3f6bb929-ef89-48df-b94d-7530285d04ae', '518376f4-f404-4b21-b370-b9e77c8ee9e0', 'White', now());


--produto 41
INSERT INTO products VALUES
('703bde7a-1649-470a-9e4f-b2172b69f6b1', 'Smartwatch with Heart Rate Monitor', 'Monitor heart rate, sleep, and fitness.', 22.00, NULL, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 70, 16, TRUE, FALSE, to_timestamp(1716658345), to_timestamp(1716658345));

INSERT INTO product_images VALUES
('aec846b8-e6db-4b37-8827-cb440f449d4f', '703bde7a-1649-470a-9e4f-b2172b69f6b1', '/assets/products-images/product_41.png', now());

INSERT INTO product_colors VALUES
('bd0e0c57-5396-49e0-987a-0e4c0f2f2a18', '703bde7a-1649-470a-9e4f-b2172b69f6b1', 'Black', now()),
('cf2d579c-0932-437e-ae18-bf5a20f3dd38', '703bde7a-1649-470a-9e4f-b2172b69f6b1', 'Red', now()),
('3db11896-f25d-4f5e-9790-7e5ebfe3cc61', '703bde7a-1649-470a-9e4f-b2172b69f6b1', 'White', now()),
('760db26b-3e06-4ce8-b792-d87ab647984e', '703bde7a-1649-470a-9e4f-b2172b69f6b1', 'Blue', now());


--produto 42
INSERT INTO products VALUES
('29b37882-86a3-4fc1-9051-ccc40ce01e2a', 'Smartwatch for Kids', 'Kid-friendly smartwatch with fun features.', 120.00, 140.00, 'bb71a3d0-59e6-47ac-913b-d3ae62a4822d', 58, 22, TRUE, FALSE, to_timestamp(1716659345), to_timestamp(1716659345));

INSERT INTO product_images VALUES
('e3dd2ce4-3b9e-44a1-a14f-4b22d4198a26', '29b37882-86a3-4fc1-9051-ccc40ce01e2a', '/assets/products-images/product_42.png', now());

INSERT INTO product_colors VALUES
('4beafc8f-cdf9-4c44-89be-6e24489d99fa', '29b37882-86a3-4fc1-9051-ccc40ce01e2a', 'Pink', now()),
('6f603ec2-1141-4f5e-bf4c-2b8f1be0a960', '29b37882-86a3-4fc1-9051-ccc40ce01e2a', 'Blue', now()),
('1f5032ff-9607-42ec-8712-bc602871e578', '29b37882-86a3-4fc1-9051-ccc40ce01e2a', 'Red', now());

