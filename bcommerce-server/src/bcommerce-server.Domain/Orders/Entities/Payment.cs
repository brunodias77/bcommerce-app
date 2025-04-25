using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders.Identifiers;
using bcommerce_server.Domain.Orders.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Entities;

public class Payment : Entity<PaymentID>
{
    private string _method;
    private decimal _amount;
    private Status _status;
    private string? _transactionId;
    private DateTime? _paidAt;
    private DateTime _createdAt;

    private Payment(PaymentID id, string method, decimal amount, Status status, string? transactionId, DateTime? paidAt, DateTime createdAt)
        : base(id)
    {
        _method = method;
        _amount = amount;
        _status = status;
        _transactionId = transactionId;
        _paidAt = paidAt;
        _createdAt = createdAt;
    }

    public static Payment Create(string method, decimal amount, Status status, string? transactionId = null)
    {
        return new Payment(PaymentID.Generate(), method, amount, status, transactionId, null, DateTime.UtcNow);
    }

    public static Payment With(PaymentID id, string method, decimal amount, Status status, string? transactionId, DateTime? paidAt, DateTime createdAt)
    {
        return new Payment(id, method, amount, status, transactionId, paidAt, createdAt);
    }

    public override void Validate(IValidationHandler handler)
    {
        new PaymentValidator(this, handler).Validate();
    }

    public string Method => _method;
    public decimal Amount => _amount;
    public Status Status => _status;
    public string? TransactionId => _transactionId;
    public DateTime? PaidAt => _paidAt;
    public DateTime CreatedAt => _createdAt;
}