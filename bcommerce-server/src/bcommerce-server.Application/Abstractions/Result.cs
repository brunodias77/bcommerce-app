namespace bcommerce_server.Application.Abstractions;

/// <summary>
/// Representa o resultado de uma operação que pode ter sucesso ou falhar,
/// inspirado no Either do Vavr e Result-oriented programming.
/// </summary>
public sealed class Result<TSuccess, TError>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public TSuccess? Success { get; }
    public TError? Error { get; }

    private Result(TSuccess success)
    {
        IsSuccess = true;
        Success = success;
        Error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Error = error;
        Success = default;
    }

    // ✅ Cria um resultado de sucesso
    public static Result<TSuccess, TError> Ok(TSuccess value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Result<TSuccess, TError>(value);
    }

    // ✅ Cria um resultado de erro
    public static Result<TSuccess, TError> Fail(TError error)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));
        return new Result<TSuccess, TError>(error);
    }

    // ✅ Match com retorno
    public TResult Match<TResult>(
        Func<TSuccess, TResult> onSuccess,
        Func<TError, TResult> onError)
    {
        return IsSuccess
            ? onSuccess(Success!)
            : onError(Error!);
    }

    // ✅ Match sem retorno (efeitos colaterais)
    public void Match(
        Action<TSuccess> onSuccess,
        Action<TError> onError)
    {
        if (IsSuccess)
            onSuccess(Success!);
        else
            onError(Error!);
    }

    // ✅ Transforma os dois lados do Result
    public Result<TNewSuccess, TNewError> Bimap<TNewSuccess, TNewError>(
        Func<TSuccess, TNewSuccess> mapSuccess,
        Func<TError, TNewError> mapError)
    {
        return IsSuccess
            ? Result<TNewSuccess, TNewError>.Ok(mapSuccess(Success!))
            : Result<TNewSuccess, TNewError>.Fail(mapError(Error!));
    }

    // ✅ Acessa o sucesso condicionalmente
    public bool TryGetSuccess(out TSuccess? success)
    {
        success = Success;
        return IsSuccess;
    }

    // ✅ Acessa o erro condicionalmente
    public bool TryGetError(out TError? error)
    {
        error = Error;
        return IsFailure;
    }

    public override string ToString()
    {
        return IsSuccess
            ? $"Success: {Success}"
            : $"Error: {Error}";
    }
}


// namespace bcommerce_server.Application.Abstractions;
//
// public class Result<TSuccess, TError>
// {
//     public bool IsSuccess { get; }
//     public TSuccess? Success { get; }
//     public TError? Error { get; }
//
//     private Result(TSuccess success)
//     {
//         IsSuccess = true;
//         Success = success;
//     }
//
//     private Result(TError error)
//     {
//         IsSuccess = false;
//         Error = error;
//     }
//
//     public static Result<TSuccess, TError> Ok(TSuccess value) => new(value);
//
//     public static Result<TSuccess, TError> Fail(TError error) => new(error);
//
//     public void Match(Action<TSuccess> onSuccess, Action<TError> onError)
//     {
//         if (IsSuccess)
//             onSuccess(Success!);
//         else
//             onError(Error!);
//     }
//
//     public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TError, TResult> onError)
//     {
//         return IsSuccess ? onSuccess(Success!) : onError(Error!);
//     }
//
//     public Result<TNewSuccess, TNewError> Bimap<TNewSuccess, TNewError>(
//         Func<TSuccess, TNewSuccess> mapSuccess,
//         Func<TError, TNewError> mapError)
//     {
//         return IsSuccess
//             ? Result<TNewSuccess, TNewError>.Ok(mapSuccess(Success!))
//             : Result<TNewSuccess, TNewError>.Fail(mapError(Error!));
//     }
// }
//
// // public class Result<TSuccess, TError>
// // {
// //     public bool IsSuccess { get; }
// //     public TSuccess? Success { get; }
// //     public TError? Error { get; }
// //
// //     private Result(TSuccess success)
// //     {
// //         IsSuccess = true;
// //         Success = success;
// //     }
// //
// //     private Result(TError error)
// //     {
// //         IsSuccess = false;
// //         Error = error;
// //     }
// //
// //     public static Result<TSuccess, TError> Ok(TSuccess value) => new(value);
// //
// //     public static Result<TSuccess, TError> Fail(TError error) => new(error);
// //
// //     public void Match(Action<TSuccess> onSuccess, Action<TError> onError)
// //     {
// //         if (IsSuccess)
// //             onSuccess(Success!);
// //         else
// //             onError(Error!);
// //     }
// //
// //     public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TError, TResult> onError)
// //     {
// //         return IsSuccess ? onSuccess(Success!) : onError(Error!);
// //     }
// // }