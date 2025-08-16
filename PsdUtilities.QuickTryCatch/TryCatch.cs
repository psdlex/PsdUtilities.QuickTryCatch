using System;
using System.Threading.Tasks;

using PsdUtilities.QuickTryCatch.Builder;

namespace PsdUtilities.QuickTryCatch;

public static partial class TryCatch
{
    public static TryCatchBuilder<object?> Execute(Action action)
    {
        Func<Task<object?>> builderAction = () =>
        {
            action();
            return Task.FromResult<object?>(null);
        };

        var builder = new TryCatchBuilder<object?>(builderAction);
        return builder;
    }

    public static TryCatchBuilder<object?> ExecuteAsync(Func<Task> action)
    {
        Func<Task<object?>> builderAction = async () =>
        {
            await action();
            return Task.FromResult<object?>(null);
        };

        var builder = new TryCatchBuilder<object?>(builderAction);
        return builder;
    }

    public static TryCatchBuilder<TResult?> Execute<TResult>(Func<TResult?> action)
    {
        Func<Task<TResult?>> builderAction = () =>
        {
            return Task.FromResult<TResult?>(action());
        };

        var builder = new TryCatchBuilder<TResult?>(builderAction);
        return builder;
    }

    public static TryCatchBuilder<TResult?> ExecuteAsync<TResult>(Func<Task<TResult?>> action)
    {
        var builder = new TryCatchBuilder<TResult?>(action);
        return builder;
    }
}