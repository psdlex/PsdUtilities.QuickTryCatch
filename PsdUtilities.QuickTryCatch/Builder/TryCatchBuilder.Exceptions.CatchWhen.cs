using System;
using PsdUtilities.QuickTryCatch.Builder.Exceptions;

namespace PsdUtilities.QuickTryCatch.Builder;

partial class TryCatchBuilder<TResult>
{
    public TryCatchBuilder<TResult> CatchAllWhen(Func<Exception, bool> predicate, ExceptionHandler<Exception>.ExceptionActionDelegate exceptionAction)
    {
        var handler = new ExceptionHandler<Exception>(exceptionAction);
        var exceptionPredicate = new ExceptionPredicate<Exception>(predicate);
        _exceptionHandlers.Add((handler, exceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchAllWhenAsync(Func<Exception, bool> predicate, AsyncExceptionHandler<Exception>.ExceptionAsyncActionDelegate exceptionAsyncAction)
    {
        var handler = new AsyncExceptionHandler<Exception>(exceptionAsyncAction);
        var exceptionPredicate = new ExceptionPredicate<Exception>(predicate);
        _exceptionHandlers.Add((handler, exceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchWhen<TException>(Func<TException, bool> predicate, ExceptionHandler<TException>.ExceptionActionDelegate exceptionAction)
        where TException : Exception
    {
        var handler = new ExceptionHandler<TException>(exceptionAction);
        var exceptionPredicate = new ExceptionPredicate<TException>(predicate);
        _exceptionHandlers.Add((handler, exceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchWhenAsync<TException>(Func<TException, bool> predicate, AsyncExceptionHandler<TException>.ExceptionAsyncActionDelegate exceptionAsyncAction)
        where TException : Exception
    {
        var handler = new AsyncExceptionHandler<TException>(exceptionAsyncAction);
        var exceptionPredicate = new ExceptionPredicate<TException>(predicate);
        _exceptionHandlers.Add((handler, exceptionPredicate));
        return this;
    }
}