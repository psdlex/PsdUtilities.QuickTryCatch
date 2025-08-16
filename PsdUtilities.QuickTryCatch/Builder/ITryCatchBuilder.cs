using System;

using PsdUtilities.QuickTryCatch.Builder.Exceptions;
using PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;

namespace PsdUtilities.QuickTryCatch.Builder;

public interface ITryCatchBuilder<TResult>
{
    TryCatchBuilder<TResult> Catch<TException>(ExceptionHandler<TException>.ExceptionActionDelegate exceptionAction) where TException : Exception;
    TryCatchBuilder<TResult> CatchAll(ExceptionHandler<Exception>.ExceptionActionDelegate exceptionAction);
    TryCatchBuilder<TResult> CatchAllAsync(AsyncExceptionHandler<Exception>.ExceptionAsyncActionDelegate exceptionAsyncAction);
    TryCatchBuilder<TResult> CatchAsync<TException>(AsyncExceptionHandler<TException>.ExceptionAsyncActionDelegate exceptionAsyncAction) where TException : Exception;
    TryCatchBuilder<TResult> CatchAllWhen(Func<Exception, bool> predicate, ExceptionHandler<Exception>.ExceptionActionDelegate exceptionAction);
    TryCatchBuilder<TResult> CatchWhen<TException>(Func<TException, bool> predicate, ExceptionHandler<TException>.ExceptionActionDelegate exceptionAction) where TException : Exception;
    TryCatchBuilder<TResult> CatchAllWhenAsync(Func<Exception, bool> predicate, AsyncExceptionHandler<Exception>.ExceptionAsyncActionDelegate exceptionAsyncAction);
    TryCatchBuilder<TResult> CatchWhenAsync<TException>(Func<TException, bool> predicate, AsyncExceptionHandler<TException>.ExceptionAsyncActionDelegate exceptionAsyncAction) where TException : Exception;
    TryCatchBuilder<TResult> ContinueWith(TryCatchBuilder<TResult>.ResultActionDelegate resultAction);
    TryCatchBuilder<TResult> ContinueWith(TryCatchBuilder<TResult>.ResultActionDelegate resultAction, ContinueOptions options);
    TryCatchBuilder<TResult> ContinueWithAsync(TryCatchBuilder<TResult>.ResultActionAsyncDelegate resultAsyncAction);
    TryCatchBuilder<TResult> ContinueWithAsync(TryCatchBuilder<TResult>.ResultActionAsyncDelegate resultAsyncAction, ContinueOptions options);
    TryCatchBuilder<TResult> Ignore<TException>() where TException : Exception;
    TryCatchBuilder<TResult> IgnoreUnhandled();
    TryCatchBuilder<TResult> RethrowUnhandled();
    TryCatchBuilder<TResult> WithRetries(int retries);
    TryCatchBuilder<TResult> WithRetries(int retries, TimeSpan delay);
}