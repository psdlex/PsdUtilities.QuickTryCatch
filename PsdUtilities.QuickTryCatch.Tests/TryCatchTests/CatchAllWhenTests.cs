using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class CatchAllWhenTests
{
    [Fact]
    public void TryCatch_CatchAllWhen_ShouldCatchWhenPredicateApproves()
    {
        var randomException = Utilities.GetRandomException();
        var num = 5;
        var action = () => { throw randomException; };
        Exception? caughtException = null;

        TryCatch.Execute(action)
            .CatchAllWhen(_ => num == 5, ex => caughtException = ex)
            .Finalize();

        caughtException?.GetType().Should().Be(randomException.GetType());
    }

    [Fact]
    public void TryCatch_CatchAllWhen_ShouldNotCatchWhenPredicateDisapproves()
    {
        var randomException = Utilities.GetRandomException();
        var num = 5;
        var action = () => { throw randomException; };
        Exception? caughtException = null;

        TryCatch.Execute(action)
            .CatchAllWhen(_ => num == 4, ex => caughtException = ex)
            .Finalize();

        caughtException?.GetType().Should().NotBe(randomException.GetType());
    }

    [Fact]
    public async Task TryCatch_CatchAllWhenAsync_ShouldCatchWhenPredicateApproves()
    {
        var randomException = Utilities.GetRandomException();
        var num = 5;
        var action = () => { throw randomException; };
        Exception? caughtException = null;

        await TryCatch.Execute(action)
            .CatchAllWhenAsync(_ => num == 5, async ex =>
            {
                await Task.Factory.StartNew(() => caughtException = ex);
            })
            .FinalizeAsync();

        caughtException?.GetType().Should().Be(randomException.GetType());
    }

    [Fact]
    public async Task TryCatch_CatchAllWhenAsync_ShouldNotCatchWhenPredicateDisapproves()
    {
        var randomException = Utilities.GetRandomException();
        var num = 5;
        var action = () => { throw randomException; };
        Exception? caughtException = null;

        await TryCatch.Execute(action)
            .CatchAllWhenAsync(_ => num == 4, async ex =>
            {
                await Task.Factory.StartNew(() => caughtException = ex);
            })
            .FinalizeAsync();

        caughtException?.GetType().Should().NotBe(randomException.GetType());
    }
}
