using bcommerce_server.Domain.Carts;
using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.Carts.Repositories;
using bcommerce_server.Infra.Data.Models.Carts;
using Dapper;

namespace bcommerce_server.Infra.Repositories;

public class DapperCartRepository : ICartRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DapperCartRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Cart cart, CancellationToken cancellationToken)
    {
        var model = CartMapper.ToDataModel(cart);

        const string insertCartSql = @"
            INSERT INTO carts (id, customer_id, created_at, updated_at)
            VALUES (@Id, @CustomerId, @CreatedAt, @UpdatedAt);
        ";
        await _unitOfWork.Connection.ExecuteAsync(insertCartSql, model, _unitOfWork.Transaction);

        foreach (var item in cart.Items)
        {
            var itemModel = CartItemMapper.ToDataModel(item, cart.Id.Value);
            const string insertItemSql = @"
                INSERT INTO cart_items 
                    (id, cart_id, product_id, quantity, added_at, unit_price, color_id)
                VALUES 
                    (@Id, @CartId, @ProductId, @Quantity, @AddedAt, @UnitPrice, @ColorId);
            ";
            await _unitOfWork.Connection.ExecuteAsync(insertItemSql, itemModel, _unitOfWork.Transaction);
        }
    }

    public async Task<Cart?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string selectCartSql = "SELECT * FROM carts WHERE id = @Id;";
        var cartModel = await _unitOfWork.Connection
            .QuerySingleOrDefaultAsync<CartDataModel>(selectCartSql, new { Id = id }, _unitOfWork.Transaction);

        if (cartModel is null) return null;

        var items = (await GetCartItemsByCartId(id, cancellationToken)).ToList();
        return CartMapper.ToDomain(cartModel, items);
    }

    public async Task<IEnumerable<Cart>> GetAll(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM carts;";
        var models = await _unitOfWork.Connection
            .QueryAsync<CartDataModel>(sql, transaction: _unitOfWork.Transaction);

        var carts = new List<Cart>();
        foreach (var model in models)
        {
            var items = (await GetCartItemsByCartId(model.Id, cancellationToken)).ToList();
            carts.Add(CartMapper.ToDomain(model, items));
        }

        return carts;
    }

    public async Task Update(Cart cart, CancellationToken cancellationToken)
    {
        var model = CartMapper.ToDataModel(cart);

        const string updateCartSql = @"
            UPDATE carts SET
                customer_id = @CustomerId,
                updated_at   = @UpdatedAt
            WHERE id = @Id;
        ";
        await _unitOfWork.Connection.ExecuteAsync(updateCartSql, model, _unitOfWork.Transaction);

        const string deleteItemsSql = "DELETE FROM cart_items WHERE cart_id = @CartId;";
        await _unitOfWork.Connection.ExecuteAsync(deleteItemsSql, new { CartId = cart.Id.Value }, _unitOfWork.Transaction);

        foreach (var item in cart.Items)
        {
            var itemModel = CartItemMapper.ToDataModel(item, cart.Id.Value);
            const string insertItemSql = @"
                INSERT INTO cart_items 
                    (id, cart_id, product_id, quantity, added_at, unit_price, color_id)
                VALUES 
                    (@Id, @CartId, @ProductId, @Quantity, @AddedAt, @UnitPrice, @ColorId);
            ";
            await _unitOfWork.Connection.ExecuteAsync(insertItemSql, itemModel, _unitOfWork.Transaction);
        }
    }

    public async Task Delete(Cart cart, CancellationToken cancellationToken)
    {
        const string deleteItemsSql = "DELETE FROM cart_items WHERE cart_id = @CartId;";
        const string deleteCartSql  = "DELETE FROM carts WHERE id = @Id;";

        await _unitOfWork.Connection.ExecuteAsync(deleteItemsSql, new { CartId = cart.Id.Value }, _unitOfWork.Transaction);
        await _unitOfWork.Connection.ExecuteAsync(deleteCartSql,  new { Id     = cart.Id.Value }, _unitOfWork.Transaction);
    }

    public async Task<Cart?> GetByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM carts WHERE customer_id = @CustomerId;";
        var cartModel = await _unitOfWork.Connection
            .QuerySingleOrDefaultAsync<CartDataModel>(sql, new { CustomerId = customerId }, _unitOfWork.Transaction);

        if (cartModel is null) return null;

        var items = (await GetCartItemsByCartId(cartModel.Id, cancellationToken)).ToList();
        return CartMapper.ToDomain(cartModel, items);
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByCartId(Guid cartId, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT id,
                   cart_id   AS CartId,
                   product_id AS ProductId,
                   color_id   AS ColorId,
                   unit_price AS UnitPrice,
                   quantity,
                   added_at   AS AddedAt
              FROM cart_items
             WHERE cart_id = @CartId;
        ";

        var itemModels = await _unitOfWork.Connection
            .QueryAsync<CartItemDataModel>(sql, new { CartId = cartId }, _unitOfWork.Transaction);

        return itemModels.Select(CartItemMapper.ToDomain);
    }
}



// using bcommerce_server.Domain.Carts;
// using bcommerce_server.Domain.Carts.Entities;
// using bcommerce_server.Domain.Carts.Repositories;
// using bcommerce_server.Infra.Data.Models.Carts;
// using Dapper;
//
// namespace bcommerce_server.Infra.Repositories;
//
// public class DapperCartRepository : ICartRepository
// {
//     private readonly IUnitOfWork _unitOfWork;
//
//     public DapperCartRepository(IUnitOfWork unitOfWork)
//     {
//         _unitOfWork = unitOfWork;
//     }
//
//     public async Task Insert(Cart cart, CancellationToken cancellationToken)
//     {
//         var model = CartMapper.ToDataModel(cart);
//
//         const string insertCartSql = @"
//             INSERT INTO carts (id, customer_id, created_at, updated_at)
//             VALUES (@Id, @CustomerId, @CreatedAt, @UpdatedAt);
//         ";
//         await _unitOfWork.Connection.ExecuteAsync(insertCartSql, model, _unitOfWork.Transaction);
//
//         foreach (var item in cart.Items)
//         {
//             var itemModel = CartItemMapper.ToDataModel(item, cart.Id.Value);
//             const string insertItemSql = @"
//                 INSERT INTO cart_items (id, cart_id, product_id, quantity, added_at)
//                 VALUES (@Id, @CartId, @ProductId, @Quantity, @AddedAt);
//             ";
//             await _unitOfWork.Connection.ExecuteAsync(insertItemSql, itemModel, _unitOfWork.Transaction);
//         }
//     }
//
//     public async Task<Cart?> Get(Guid id, CancellationToken cancellationToken)
//     {
//         const string selectCartSql = "SELECT * FROM carts WHERE id = @Id;";
//         var cartModel = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CartDataModel>(
//             selectCartSql, new { Id = id }, _unitOfWork.Transaction);
//
//         if (cartModel is null) return null;
//
//         var itemModels = await _unitOfWork.Connection.QueryAsync<CartItemDataModel>(
//             "SELECT * FROM cart_items WHERE cart_id = @CartId;",
//             new { CartId = id }, _unitOfWork.Transaction);
//
//         var items = itemModels.Select(CartItemMapper.ToDomain).ToList();
//
//         return CartMapper.ToDomain(cartModel, items);
//     }
//
//     public async Task<IEnumerable<Cart>> GetAll(CancellationToken cancellationToken)
//     {
//         const string sql = "SELECT * FROM carts;";
//         var models = await _unitOfWork.Connection.QueryAsync<CartDataModel>(sql, transaction: _unitOfWork.Transaction);
//
//         var carts = new List<Cart>();
//         foreach (var model in models)
//         {
//             var itemModels = await _unitOfWork.Connection.QueryAsync<CartItemDataModel>(
//                 "SELECT * FROM cart_items WHERE cart_id = @CartId;",
//                 new { CartId = model.Id }, _unitOfWork.Transaction);
//
//             var items = itemModels.Select(CartItemMapper.ToDomain).ToList();
//             carts.Add(CartMapper.ToDomain(model, items));
//         }
//
//         return carts;
//     }
//
//     public async Task Update(Cart cart, CancellationToken cancellationToken)
//     {
//         var model = CartMapper.ToDataModel(cart);
//
//         const string updateSql = @"
//             UPDATE carts SET
//                 customer_id = @CustomerId,
//                 updated_at = @UpdatedAt
//             WHERE id = @Id;
//         ";
//         await _unitOfWork.Connection.ExecuteAsync(updateSql, model, _unitOfWork.Transaction);
//
//         const string deleteItemsSql = "DELETE FROM cart_items WHERE cart_id = @CartId;";
//         await _unitOfWork.Connection.ExecuteAsync(deleteItemsSql, new { CartId = cart.Id.Value }, _unitOfWork.Transaction);
//
//         foreach (var item in cart.Items)
//         {
//             var itemModel = CartItemMapper.ToDataModel(item, cart.Id.Value);
//             const string insertItemSql = @"
//                 INSERT INTO cart_items (id, cart_id, product_id, quantity, added_at)
//                 VALUES (@Id, @CartId, @ProductId, @Quantity, @AddedAt);
//             ";
//             await _unitOfWork.Connection.ExecuteAsync(insertItemSql, itemModel, _unitOfWork.Transaction);
//         }
//     }
//
//     public async Task Delete(Cart cart, CancellationToken cancellationToken)
//     {
//         const string deleteItemsSql = "DELETE FROM cart_items WHERE cart_id = @CartId;";
//         const string deleteCartSql = "DELETE FROM carts WHERE id = @Id;";
//
//         await _unitOfWork.Connection.ExecuteAsync(deleteItemsSql, new { CartId = cart.Id.Value }, _unitOfWork.Transaction);
//         await _unitOfWork.Connection.ExecuteAsync(deleteCartSql, new { Id = cart.Id.Value }, _unitOfWork.Transaction);
//     }
//
//     public async Task<Cart?> GetByCustomerId(Guid customerId, CancellationToken cancellationToken)
//     {
//         const string sql = "SELECT * FROM carts WHERE customer_id = @CustomerId;";
//         var cartModel = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CartDataModel>(
//             sql, new { CustomerId = customerId }, _unitOfWork.Transaction);
//         if (cartModel is null) return null;
//         var itemModels = await _unitOfWork.Connection.QueryAsync<CartItemDataModel>(
//             "SELECT * FROM cart_items WHERE cart_id = @CartId;",
//             new { CartId = cartModel.Id }, _unitOfWork.Transaction);
//         var items = itemModels.Select(CartItemMapper.ToDomain).ToList();
//         return CartMapper.ToDomain(cartModel, items);    }
//
//     public Task<IEnumerable<CartItem>> GetCartItemsByCartId(Guid cartId, CancellationToken cancellationToken)
//     {
//         var sql = "SELECT * FROM cart_items WHERE cart_id = @CartId;";
//         throw new NotImplementedException();
//     }
// }