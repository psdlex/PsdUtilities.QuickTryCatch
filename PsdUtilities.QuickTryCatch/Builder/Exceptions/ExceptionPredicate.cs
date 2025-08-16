using System;

namespace PsdUtilities.QuickTryCatch.Builder.Exceptions;

public sealed class ExceptionPredicate<TException>(Func<TException, bool> func) : IExceptionPredicate
    where TException : Exception
{
    public bool Approve(Exception? exception)
    {
        if (exception is not TException typedException)
            return false;

        return func(typedException);
    }
}

public interface IExceptionPredicate
{
    bool Approve(Exception? exception);
}