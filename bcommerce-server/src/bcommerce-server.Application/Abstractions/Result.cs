namespace bcommerce_server.Application.Abstractions;

public class Result<TSuccess, TError>
{
    public bool IsSuccess { get; }
    public TSuccess? Success { get; }
    public TError? Error { get; }

    private Result(TSuccess success)
    {
        IsSuccess = true;
        Success = success;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<TSuccess, TError> Ok(TSuccess value) => new(value);

    public static Result<TSuccess, TError> Fail(TError error) => new(error);

    public void Match(Action<TSuccess> onSuccess, Action<TError> onError)
    {
        if (IsSuccess)
            onSuccess(Success!);
        else
            onError(Error!);
    }

    public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TError, TResult> onError)
    {
        return IsSuccess ? onSuccess(Success!) : onError(Error!);
    }
}