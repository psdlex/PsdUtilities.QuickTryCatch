using System;
using System.Threading.Tasks;

namespace PsdUtilities.QuickTryCatch.Builder.Exceptions;

public interface IExceptionHandler
{
    Type ExceptionType { get; }
    void Handle(Exception exception);
}

public interface IAsyncExceptionHandler : IExceptionHandler
{
    Task HandleAsync(Exception exception);
}