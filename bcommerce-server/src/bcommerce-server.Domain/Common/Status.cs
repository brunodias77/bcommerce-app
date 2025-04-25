using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Common;

/// <summary>
/// Representa um status genérico tipado no domínio.
/// Evita uso de strings soltas para estados de entidade.
/// </summary>
public sealed class Status : ValueObject
{
    private static readonly HashSet<string> _validOrderStatuses = new()
    {
        Pending, Confirmed, Shipped, Delivered, Cancelled
    };

    private static readonly HashSet<string> _validPaymentStatuses = new()
    {
        Pending, Paid, Failed
    };

    private static readonly HashSet<string> _validShipmentStatuses = new()
    {
        Ready, Shipped, Delivered
    };

    public string Value { get; }

    private Status(string value)
    {
        Value = value;
    }

    public static Status Order(string value)
    {
        if (!_validOrderStatuses.Contains(value.ToLower()))
            throw new ArgumentException($"Status '{value}' inválido para pedido.");

        return new Status(value.ToLower());
    }

    public static Status Payment(string value)
    {
        if (!_validPaymentStatuses.Contains(value.ToLower()))
            throw new ArgumentException($"Status '{value}' inválido para pagamento.");

        return new Status(value.ToLower());
    }

    public static Status Shipment(string value)
    {
        if (!_validShipmentStatuses.Contains(value.ToLower()))
            throw new ArgumentException($"Status '{value}' inválido para envio.");

        return new Status(value.ToLower());
    }

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    // Pré-definidos para legibilidade
    public const string Pending = "pending";
    public const string Confirmed = "confirmed";
    public const string Paid = "paid";
    public const string Failed = "failed";
    public const string Shipped = "shipped";
    public const string Delivered = "delivered";
    public const string Cancelled = "cancelled";
    public const string Ready = "ready";
}