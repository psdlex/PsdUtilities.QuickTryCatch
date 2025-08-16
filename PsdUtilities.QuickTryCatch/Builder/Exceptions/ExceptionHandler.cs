using System;

namespace PsdUtilities.QuickTryCatch.Builder.Exceptions;

public sealed class ExceptionHandler<TException> : IExceptionHandler
{
    public delegate void ExceptionActionDelegate(TException exception);

    private readonly ExceptionActionDelegate _action;

    public Type ExceptionType { get; } = typeof(TException);

    public ExceptionHandler(ExceptionActionDelegate action)
    {
        _action = action;
    }

    public void Handle(Exception ex)
    {
        if (ex is TException typedException)
            _action(typedException);
    }
}