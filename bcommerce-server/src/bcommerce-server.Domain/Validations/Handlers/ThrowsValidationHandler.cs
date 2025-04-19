using bcommerce_server.Domain.Exceptions;

namespace bcommerce_server.Domain.Validations.Handlers;


    /// <summary>
    /// Handler que lança exceções imediatamente ao detectar erros de validação.
    /// Ideal para cenários fail-fast.
    /// </summary>
    public class ThrowsValidationHandler : IValidationHandler
    {
        public IValidationHandler Append(Error error)
        {
            throw DomainException.With(error);
        }

        public IValidationHandler Append(IValidationHandler handler)
        {
            throw DomainException.With(handler.GetErrors());
        }

        public T Validate<T>(IValidation<T> validation)
        {
            try
            {
                return validation.Validate();
            }
            catch (Exception ex)
            {
                throw DomainException.With(new Error(ex.Message));
            }
        }

        public IReadOnlyList<Error> GetErrors() => Array.Empty<Error>();
    }
