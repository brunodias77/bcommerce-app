using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Exceptions;

/// <summary>
/// Exceção específica do domínio que representa falhas de validação ou regras de negócio.
/// Mantém uma lista de erros detalhados para permitir tratamento específico de problemas de validação.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Lista de erros associados à exceção.
    /// </summary>
    public IReadOnlyList<Error> Errors { get; }

    /// <summary>
    /// Construtor protegido. Use os métodos estáticos With() para instanciar.
    /// </summary>
    protected DomainException(string message, List<Error> errors)
        : base(message)
    {
        Errors = errors ?? new List<Error>();
    }

    /// <summary>
    /// Cria uma exceção com um único erro de validação.
    /// </summary>
    public static DomainException With(Error error)
    {
        return new DomainException(error.Message, new List<Error> { error });
    }

    /// <summary>
    /// Cria uma exceção com múltiplos erros de validação.
    /// </summary>
    public static DomainException With(IEnumerable<Error> errors)
    {
        return new DomainException(string.Empty, new List<Error>(errors));
    }
}