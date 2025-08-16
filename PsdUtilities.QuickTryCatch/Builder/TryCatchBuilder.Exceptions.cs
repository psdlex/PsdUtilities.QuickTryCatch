using System;
using System.Collections.Generic;
using PsdUtilities.QuickTryCatch.Builder.Exceptions;

using PredicateHandler = (PsdUtilities.QuickTryCatch.Builder.Exceptions.IExceptionHandler Handler, PsdUtilities.QuickTryCatch.Builder.Exceptions.IExceptionPredicate Predicate);

namespace PsdUtilities.QuickTryCatch.Builder;

partial class TryCatchBuilder<TResult>
{
    private static readonly IExceptionPredicate _emptyExceptionPredicate = new ExceptionPredicate<Exception>(_ => true);

    private readonly List<PredicateHandler> _exceptionHandlers = [];
    private readonly List<Type> _ignoredExceptionTypes = [];

    internal IReadOnlyList<PredicateHandler> ExceptionHandlers => _exceptionHandlers;
    internal IReadOnlyList<Type> IgnoredExceptionTypes => _ignoredExceptionTypes;

    internal UnhandledExceptionBehavior UnhandledExceptionBehavior { get; private set; } = UnhandledExceptionBehavior.Ignore;

    public TryCatchBuilder<TResult> RethrowUnhandled()
    {
        UnhandledExceptionBehavior = UnhandledExceptionBehavior.Rethrow;
        return this;
    }

    public TryCatchBuilder<TResult> IgnoreUnhandled()
    {
        UnhandledExceptionBehavior = UnhandledExceptionBehavior.Ignore;
        return this;
    }

    public TryCatchBuilder<TResult> Ignore<TException>() where TException : Exception
    {
        _ignoredExceptionTypes.Add(typeof(TException));
        return this;
    }
}