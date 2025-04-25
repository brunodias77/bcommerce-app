using bcommerce_server.Domain.Orders;
using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Repositories;
using bcommerce_server.Infra.Data.Models.Orders;
using Dapper;

namespace bcommerce_server.Infra.Repositories;

public class DapperOrderRepository : IOrderRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DapperOrderRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Order order, CancellationToken cancellationToken)
    {
        var model = OrderMapper.ToDataModel(order);

        const string sql = @"
            INSERT INTO orders (
                id, customer_id, shipping_address_id, coupon_id, status,
                total_amount, placed_at, shipped_at, delivered_at,
                created_at, updated_at
            ) VALUES (
                @Id, @CustomerId, @ShippingAddressId, @CouponId, @Status,
                @TotalAmount, @PlacedAt, @ShippedAt, @DeliveredAt,
                @CreatedAt, @UpdatedAt
            );
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);

        // ✅ Passa o order.Id.Value para os mappers
        foreach (var item in order.Items)
        {
            var itemModel = OrderItemMapper.ToDataModel(item, order.Id.Value);
            const string itemSql = @"
                INSERT INTO order_items (id, order_id, product_id, quantity, unit_price, created_at)
                VALUES (@Id, @OrderId, @ProductId, @Quantity, @UnitPrice, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(itemSql, itemModel, _unitOfWork.Transaction);
        }

        if (order.Payment is not null)
        {
            var paymentModel = PaymentMapper.ToDataModel(order.Payment, order.Id.Value);
            const string paymentSql = @"
                INSERT INTO payments (id, order_id, method, amount, status, transaction_id, paid_at, created_at)
                VALUES (@Id, @OrderId, @Method, @Amount, @Status, @TransactionId, @PaidAt, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(paymentSql, paymentModel, _unitOfWork.Transaction);
        }

        if (order.Shipment is not null)
        {
            var shipmentModel = ShipmentMapper.ToDataModel(order.Shipment, order.Id.Value);
            const string shipmentSql = @"
                INSERT INTO shipments (id, order_id, carrier, tracking_number, status, shipped_at, delivered_at, created_at)
                VALUES (@Id, @OrderId, @Carrier, @TrackingNumber, @Status, @ShippedAt, @DeliveredAt, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(shipmentSql, shipmentModel, _unitOfWork.Transaction);
        }
    }


    public async Task<Order?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM orders WHERE id = @Id;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<OrderDataModel>(
            sql, new { Id = id }, _unitOfWork.Transaction);

        if (model is null) return null;

        var items = await LoadItems(model.Id);
        var payment = await LoadPayment(model.Id);
        var shipment = await LoadShipment(model.Id);

        return OrderMapper.ToDomain(model, items, payment, shipment);
    }

    public async Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM orders;";
        var models = await _unitOfWork.Connection.QueryAsync<OrderDataModel>(sql, transaction: _unitOfWork.Transaction);

        var orders = new List<Order>();
        foreach (var model in models)
        {
            var items = await LoadItems(model.Id);
            var payment = await LoadPayment(model.Id);
            var shipment = await LoadShipment(model.Id);

            orders.Add(OrderMapper.ToDomain(model, items, payment, shipment));
        }

        return orders;
    }

    public async Task<IEnumerable<Order>> GetByCustomer(Guid customerId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM orders WHERE customer_id = @CustomerId;";
        var models = await _unitOfWork.Connection.QueryAsync<OrderDataModel>(
            sql, new { CustomerId = customerId }, _unitOfWork.Transaction);

        var orders = new List<Order>();
        foreach (var model in models)
        {
            var items = await LoadItems(model.Id);
            var payment = await LoadPayment(model.Id);
            var shipment = await LoadShipment(model.Id);

            orders.Add(OrderMapper.ToDomain(model, items, payment, shipment));
        }

        return orders;
    }
    
    public async Task Update(Order order, CancellationToken cancellationToken)
    {
        var model = OrderMapper.ToDataModel(order);

        const string updateOrderSql = @"
            UPDATE orders SET
                customer_id = @CustomerId,
                shipping_address_id = @ShippingAddressId,
                coupon_id = @CouponId,
                status = @Status,
                total_amount = @TotalAmount,
                placed_at = @PlacedAt,
                shipped_at = @ShippedAt,
                delivered_at = @DeliveredAt,
                updated_at = @UpdatedAt
            WHERE id = @Id;
        ";

        await _unitOfWork.Connection.ExecuteAsync(updateOrderSql, model, _unitOfWork.Transaction);

        // --- OrderItems (Remover tudo e reinserir)
        const string deleteItemsSql = "DELETE FROM order_items WHERE order_id = @OrderId;";
        await _unitOfWork.Connection.ExecuteAsync(deleteItemsSql, new { OrderId = order.Id.Value }, _unitOfWork.Transaction);

        foreach (var item in order.Items)
        {
            var itemModel = OrderItemMapper.ToDataModel(item, order.Id.Value);
            const string insertItemSql = @"
                INSERT INTO order_items (id, order_id, product_id, quantity, unit_price, created_at)
                VALUES (@Id, @OrderId, @ProductId, @Quantity, @UnitPrice, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(insertItemSql, itemModel, _unitOfWork.Transaction);
        }

        // --- Payment (delete + insert)
        const string deletePaymentSql = "DELETE FROM payments WHERE order_id = @OrderId;";
        await _unitOfWork.Connection.ExecuteAsync(deletePaymentSql, new { OrderId = order.Id.Value }, _unitOfWork.Transaction);

        if (order.Payment is not null)
        {
            var paymentModel = PaymentMapper.ToDataModel(order.Payment, order.Id.Value);
            const string insertPaymentSql = @"
                INSERT INTO payments (id, order_id, method, amount, status, transaction_id, paid_at, created_at)
                VALUES (@Id, @OrderId, @Method, @Amount, @Status, @TransactionId, @PaidAt, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(insertPaymentSql, paymentModel, _unitOfWork.Transaction);
        }

        // --- Shipment (delete + insert)
        const string deleteShipmentSql = "DELETE FROM shipments WHERE order_id = @OrderId;";
        await _unitOfWork.Connection.ExecuteAsync(deleteShipmentSql, new { OrderId = order.Id.Value }, _unitOfWork.Transaction);

        if (order.Shipment is not null)
        {
            var shipmentModel = ShipmentMapper.ToDataModel(order.Shipment, order.Id.Value);
            const string insertShipmentSql = @"
                INSERT INTO shipments (id, order_id, carrier, tracking_number, status, shipped_at, delivered_at, created_at)
                VALUES (@Id, @OrderId, @Carrier, @TrackingNumber, @Status, @ShippedAt, @DeliveredAt, @CreatedAt);
            ";
            await _unitOfWork.Connection.ExecuteAsync(insertShipmentSql, shipmentModel, _unitOfWork.Transaction);
        }
    }


    // public async Task Update(Order order, CancellationToken cancellationToken)
    // {
    //     var model = OrderMapper.ToDataModel(order);
    //     const string sql = @"
    //         UPDATE orders SET
    //             customer_id = @CustomerId,
    //             shipping_address_id = @ShippingAddressId,
    //             coupon_id = @CouponId,
    //             status = @Status,
    //             total_amount = @TotalAmount,
    //             placed_at = @PlacedAt,
    //             shipped_at = @ShippedAt,
    //             delivered_at = @DeliveredAt,
    //             updated_at = @UpdatedAt
    //         WHERE id = @Id;
    //     ";
    //
    //     await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    // }

    public async Task Delete(Order order, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM orders WHERE id = @Id;";
        await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = order.Id.Value }, _unitOfWork.Transaction);
    }

    private async Task<List<OrderItem>> LoadItems(Guid orderId)
    {
        const string sql = "SELECT * FROM order_items WHERE order_id = @OrderId;";
        var models = await _unitOfWork.Connection.QueryAsync<OrderItemDataModel>(sql, new { OrderId = orderId }, _unitOfWork.Transaction);
        return models.Select(OrderItemMapper.ToDomain).ToList();
    }

    private async Task<Payment?> LoadPayment(Guid orderId)
    {
        const string sql = "SELECT * FROM payments WHERE order_id = @OrderId;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<PaymentDataModel>(sql, new { OrderId = orderId }, _unitOfWork.Transaction);
        return model is null ? null : PaymentMapper.ToDomain(model);
    }

    private async Task<Shipment?> LoadShipment(Guid orderId)
    {
        const string sql = "SELECT * FROM shipments WHERE order_id = @OrderId;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<ShipmentDataModel>(sql, new { OrderId = orderId }, _unitOfWork.Transaction);
        return model is null ? null : ShipmentMapper.ToDomain(model);
    }
}
