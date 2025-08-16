using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PsdUtilities.QuickTryCatch.Builder;
using PsdUtilities.QuickTryCatch.Builder.Exceptions;
using PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;
using PsdUtilities.QuickTryCatch.TryCatchResult;

using PredicateHandler = (PsdUtilities.QuickTryCatch.Builder.Exceptions.IExceptionHandler Handler, PsdUtilities.QuickTryCatch.Builder.Exceptions.IExceptionPredicate Predicate);

namespace PsdUtilities.QuickTryCatch;

static partial class TryCatch
{
    public static TryCatchResult<TResult> Finalize<TResult>(this TryCatchBuilder<TResult> builder) => FinalizeAsync(builder).GetAwaiter().GetResult();
    public static async Task<TryCatchResult<TResult>> FinalizeAsync<TResult>(this TryCatchBuilder<TResult> builder)
    {
        TryCatchResult<TResult>? result = null;

        do
        {
            if (result is not null && builder.RetryDelay != TimeSpan.Zero)
                await Task.Delay(builder.RetryDelay);

            result = await FinalizeAsyncInternal(builder, result?.ExecutionRetries + 1 ?? 0);

        } while (result.Success == false && result.ExecutionRetries < builder.Retries);

        return result;
    }

    private static async Task<TryCatchResult<TResult>> FinalizeAsyncInternal<TResult>(TryCatchBuilder<TResult> builder, int retries = 0)
    {
        try
        {
            var resultValue = await builder.Action();
            var actions = GetResultActions(builder);

            foreach (var action in actions)
                await action(resultValue);

            return new TryCatchResult<TResult>(resultValue, true, null, retries);
        }
        catch (Exception ex)
        {
            var exceptionType = ex.GetType();

            // ignored
            if (builder.IgnoredExceptionTypes.Contains(exceptionType))
                return TryCatchResult<TResult>.Failed(ex, retries);

            var predicateHandlers = GetExceptionHandlers(builder, exceptionType);

            IExceptionHandler handler;

            if (predicateHandlers.Specific?.Handler is not null && predicateHandlers.Specific.Value.Predicate.Approve(ex))
                handler = predicateHandlers.Specific.Value.Handler;

            else if (predicateHandlers.General?.Handler is not null && predicateHandlers.General.Value.Predicate.Approve(ex))
                handler = predicateHandlers.General.Value.Handler;

            else
            {
                switch (builder.UnhandledExceptionBehavior)
                {
                    case UnhandledExceptionBehavior.Rethrow: throw;
                    default: return TryCatchResult<TResult>.Failed(ex, retries);
                }
            }

            if (handler is IAsyncExceptionHandler asyncHandler)
                await asyncHandler.HandleAsync(ex);
            else
                handler.Handle(ex);

            return TryCatchResult<TResult>.Failed(ex, retries);
        }
    }

    private static (PredicateHandler? Specific, PredicateHandler? General) GetExceptionHandlers<TResult>(TryCatchBuilder<TResult> builder, Type exceptionType)
    {
        var handlers = builder
            .ExceptionHandlers
            .Where(h => h.Handler.ExceptionType.IsAssignableFrom(exceptionType))
            .ToList();

        if (handlers.Count == 0)
            return (null, null);

        var lookup = handlers.ToLookup(h => h.Handler.ExceptionType == typeof(Exception));
        var generalHandler = lookup[true].FirstOrDefault();
        var specificHandler = lookup[false].FirstOrDefault();

        return (specificHandler, generalHandler);
    }

    private static IEnumerable<TryCatchBuilder<TResult>.ResultActionAsyncDelegate> GetResultActions<TResult>(TryCatchBuilder<TResult> builder)
    {
        var firsts = builder
                .ResultActions
                .Where(x => x.Options.ExecutionOrder.Definition == ExecutionOrderDefinition.First)
                .Select(x => x.ResultAction);

        var sequential = builder
                .ResultActions
                .Where(x => x.Options.ExecutionOrder.Definition == ExecutionOrderDefinition.ValueBased)
                .OrderBy(x => x.Options.ExecutionOrder.OrderValue)
                .Select(x => x.ResultAction);

        var lasts = builder
                .ResultActions
                .Where(x => x.Options.ExecutionOrder.Definition == ExecutionOrderDefinition.Last)
                .Select(x => x.ResultAction);

        return firsts.Concat(sequential).Concat(lasts);
    }
}