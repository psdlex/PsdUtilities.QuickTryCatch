using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class CatchWhenTests
{
    [Fact]
    public void TryCatch_CatchWhen_ShouldCatchWhenPredicateApproves()
    {
        var exception = new InvalidOperationException();
        var num = 5;
        var action = () => { throw exception; };
        var exceptionCaught = false;

        TryCatch.Execute(action)
            .CatchWhen<InvalidOperationException>(_ => num == 5, _ => exceptionCaught = true)
            .Finalize();

        exceptionCaught.Should().BeTrue();
    }

    [Fact]
    public void TryCatch_CatchWhen_ShouldNotCatchWhenPredicateDisapproves()
    {
        var exception = new InvalidOperationException();
        var num = 5;
        var action = () => { throw exception; };
        var exceptionCaught = false;

        TryCatch.Execute(action)
            .CatchWhen<InvalidOperationException>(_ => num == 4, _ => exceptionCaught = true)
            .Finalize();

        exceptionCaught.Should().BeFalse();
    }

    [Fact]
    public async Task TryCatch_CatchWhenAsync_ShouldCatchWhenPredicateApproves()
    {
        var exception = new InvalidOperationException();
        var num = 5;
        var action = () => { throw exception; };
        var exceptionCaught = false;

        await TryCatch.Execute(action)
            .CatchWhenAsync<InvalidOperationException>(_ => num == 5, async _ =>
            {
                await Task.Factory.StartNew(() => exceptionCaught = true);
            })
            .FinalizeAsync();

        exceptionCaught.Should().BeTrue();
    }

    [Fact]
    public async Task TryCatch_CatchWhenAsync_ShouldNotCatchWhenPredicateDisapproves()
    {
        var exception = new InvalidOperationException();
        var num = 5;
        var action = () => { throw exception; };
        var exceptionCaught = false;

        await TryCatch.Execute(action)
            .CatchWhenAsync<InvalidOperationException>(_ => num == 4, async _ =>
            {
                await Task.Factory.StartNew(() => exceptionCaught = true);
            })
            .FinalizeAsync();

        exceptionCaught.Should().BeFalse();
    }
}