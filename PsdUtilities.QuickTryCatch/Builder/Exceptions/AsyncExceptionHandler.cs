using System;
using System.Threading.Tasks;

namespace PsdUtilities.QuickTryCatch.Builder.Exceptions;

public sealed class AsyncExceptionHandler<TException> : IAsyncExceptionHandler
    where TException : Exception
{
    public delegate Task ExceptionAsyncActionDelegate(TException exception);

    private readonly ExceptionAsyncActionDelegate _asyncAction;

    public Type ExceptionType { get; } = typeof(TException);

    public AsyncExceptionHandler(ExceptionAsyncActionDelegate asyncAction)
    {
        _asyncAction = asyncAction;
    }

    public void Handle(Exception exception) => HandleAsync(exception).GetAwaiter().GetResult();
    public async Task HandleAsync(Exception exception)
    {
        if (exception is TException typedException)
            await _asyncAction(typedException);
    }
}