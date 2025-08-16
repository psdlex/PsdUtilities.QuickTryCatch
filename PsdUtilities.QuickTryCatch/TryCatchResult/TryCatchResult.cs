using System;

namespace PsdUtilities.QuickTryCatch.TryCatchResult;

public sealed record TryCatchResult<TResult>(TResult? Value, bool Success, Exception? Exception, int ExecutionRetries)
{
    public static TryCatchResult<TResult> Failed(Exception exception, int retries) => new(default, false, exception, retries);
}