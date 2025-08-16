using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class IgnoreTests
{
    [Fact]
    public void TryCatch_Ignore_ShouldIgnoreSpecificException()
    {
        var exception = new NullReferenceException();
        var action = () => { throw exception; };
        var exceptionHandled = false;

        TryCatch.Execute(action)
            .Catch<NullReferenceException>(_ => exceptionHandled = true)
            .Ignore<NullReferenceException>()
            .Finalize();

        exceptionHandled.Should().BeFalse();
    }

    [Fact]
    public void TryCatch_RethrowUnhandled_ShouldRethrowUnhandledExceptions()
    {
        var exception = new InvalidOperationException();
        var action = () => { throw exception; };
        var exceptionHandled = false;
        var rethrown = false;

        try
        {
            TryCatch.Execute(action)
                .Catch<NullReferenceException>(_ => exceptionHandled = true)
                .RethrowUnhandled()
                .Finalize();
        } catch
        {
            rethrown = true;
        }

        rethrown.Should().BeTrue();
        exceptionHandled.Should().BeFalse();
    }
}
