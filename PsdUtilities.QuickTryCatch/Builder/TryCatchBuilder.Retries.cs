using System;

namespace PsdUtilities.QuickTryCatch.Builder;

partial class TryCatchBuilder<TResult>
{
    internal int Retries { get; private set; }
    internal TimeSpan RetryDelay { get; private set; }

    public TryCatchBuilder<TResult> WithRetries(int retries) => WithRetries(retries, TimeSpan.Zero);
    public TryCatchBuilder<TResult> WithRetries(int retries, TimeSpan delay)
    {
        Retries = retries;
        RetryDelay = delay;
        return this;
    }
}