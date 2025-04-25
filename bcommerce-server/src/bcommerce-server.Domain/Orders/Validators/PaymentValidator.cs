using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Validators;

public class PaymentValidator : Validator
{
    private readonly Payment _payment;

    public PaymentValidator(Payment payment, IValidationHandler handler)
        : base(handler)
    {
        _payment = payment ?? throw new ArgumentNullException(nameof(payment));
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_payment.Method))
        {
            AddError("'method' de pagamento não pode estar em branco.");
        }

        if (_payment.Amount <= 0)
        {
            AddError("'amount' deve ser maior que 0.");
        }

        if (_payment.Status is null)
        {
            AddError("'status' não pode ser nulo.");
        }

        if (_payment.TransactionId?.Length > 100)
        {
            AddError("'transactionId' não pode ter mais que 100 caracteres.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}