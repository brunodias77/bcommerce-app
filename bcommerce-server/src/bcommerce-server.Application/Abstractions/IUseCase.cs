namespace bcommerce_server.Application.Abstractions;


public interface IUseCase<in TInput, TSuccess, TError>
{
    Task<Result<TSuccess, TError>> Execute(TInput input);
}