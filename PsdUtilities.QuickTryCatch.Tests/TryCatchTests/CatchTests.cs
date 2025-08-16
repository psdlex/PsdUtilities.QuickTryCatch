using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class CatchTests
{
    [Fact]
    public void TryCatch_Catch_ShouldCatchGenericException()
    {
        var exception = new ArgumentException();
        var action = () => { throw exception; };
        var exceptionHandled = false;

        TryCatch.Execute(action)
            .Catch<ArgumentException>(_ => exceptionHandled = true)
            .Finalize();

        exceptionHandled.Should().BeTrue();
    }

    [Fact]
    public async Task TryCatch_CatchAsync_ShouldCatchGenericException()
    {
        var exception = new IOException();
        var action = () => { throw exception; };
        var exceptionHandled = false;

        await TryCatch.Execute(action)
            .CatchAsync<IOException>(async _ =>
            {
                await Task.Factory.StartNew(() => exceptionHandled = true);
            })
            .FinalizeAsync();

        exceptionHandled.Should().BeTrue();
    }
}
