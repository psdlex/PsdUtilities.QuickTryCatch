using System;
using System.Threading.Tasks;

using PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;

namespace PsdUtilities.QuickTryCatch.Builder;

public partial class TryCatchBuilder<TResult> : ITryCatchBuilder<TResult>
{
    private readonly ContinueOptionFactory _continueOptionFactory;

    internal Func<Task<TResult>> Action { get; }

    public TryCatchBuilder(Func<Task<TResult>> action)
    {
        Action = action;
        _continueOptionFactory = new();
    }
}