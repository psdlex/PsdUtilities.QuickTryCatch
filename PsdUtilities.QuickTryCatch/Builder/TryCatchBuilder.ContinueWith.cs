using System.Collections.Generic;
using System.Threading.Tasks;

using PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;

namespace PsdUtilities.QuickTryCatch.Builder;

partial class TryCatchBuilder<TResult>
{
    public delegate void ResultActionDelegate(TResult? resultAction);
    public delegate Task ResultActionAsyncDelegate(TResult? resultAction);

    private readonly List<(ResultActionAsyncDelegate ResultAction, ContinueOptions Options)> _resultActions = [];

    internal IReadOnlyList<(ResultActionAsyncDelegate ResultAction, ContinueOptions Options)> ResultActions => _resultActions;

    public TryCatchBuilder<TResult> ContinueWith(ResultActionDelegate resultAction) => ContinueWith(resultAction, _continueOptionFactory.CreateDefault());
    public TryCatchBuilder<TResult> ContinueWith(ResultActionDelegate resultAction, ContinueOptions options)
    {
        ResultActionAsyncDelegate action = (result) =>
        {
            resultAction(result);
            return Task.CompletedTask;
        };

        _resultActions.Add((action, options));
        return this;
    }

    public TryCatchBuilder<TResult> ContinueWithAsync(ResultActionAsyncDelegate resultAsyncAction) => ContinueWithAsync(resultAsyncAction, _continueOptionFactory.CreateDefault());
    public TryCatchBuilder<TResult> ContinueWithAsync(ResultActionAsyncDelegate resultAsyncAction, ContinueOptions options)
    {
        _resultActions.Add((resultAsyncAction, options));
        return this;
    }
}