using System;
using PsdUtilities.QuickTryCatch.Builder.Exceptions;

namespace PsdUtilities.QuickTryCatch.Builder;

partial class TryCatchBuilder<TResult>
{
    public TryCatchBuilder<TResult> Catch<TException>(ExceptionHandler<TException>.ExceptionActionDelegate exceptionAction) where TException : Exception
    {
        var handler = new ExceptionHandler<TException>(exceptionAction);
        _exceptionHandlers.Add((handler, _emptyExceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchAsync<TException>(AsyncExceptionHandler<TException>.ExceptionAsyncActionDelegate exceptionAsyncAction) where TException : Exception
    {
        var handler = new AsyncExceptionHandler<TException>(exceptionAsyncAction);
        _exceptionHandlers.Add((handler, _emptyExceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchAll(ExceptionHandler<Exception>.ExceptionActionDelegate exceptionAction)
    {
        var handler = new ExceptionHandler<Exception>(exceptionAction);
        _exceptionHandlers.Add((handler, _emptyExceptionPredicate));
        return this;
    }

    public TryCatchBuilder<TResult> CatchAllAsync(AsyncExceptionHandler<Exception>.ExceptionAsyncActionDelegate exceptionAsyncAction)
    {
        var handler = new AsyncExceptionHandler<Exception>(exceptionAsyncAction);
        _exceptionHandlers.Add((handler, _emptyExceptionPredicate));
        return this;
    }
}