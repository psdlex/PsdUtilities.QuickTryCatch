using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class ContinueWithTests
{
    [Fact]
    public void TryCatch_ContinueWith_ShouldContinueWhenActionSucceeds_WithoutResult()
    {
        var succeeded = false;
        var action = () => { succeeded = true; };
        var continued = false;

        TryCatch.Execute(action)
            .ContinueWith(_ => continued = true)
            .Finalize();

        succeeded.Should().BeTrue();
        continued.Should().BeTrue();
    }

    [Fact]
    public void TryCatch_ContinueWith_ShouldNotContinueWhenActionThrows_WithoutResult()
    {
        var succeeded = false;
        var exception = new InvalidOperationException();
        var action = () => { succeeded = true; throw exception; };
        var continued = false;

        TryCatch.Execute(action)
            .ContinueWith(_ => continued = true)
            .Finalize();

        succeeded.Should().BeTrue();
        continued.Should().BeFalse();
    }

    [Fact]
    public void TryCatch_ContinueWith_ShouldContinueWhenActionSucceeds_WithResult()
    {
        var returnValue = "Hello world";
        var succeeded = false;
        var action = () => { succeeded = true; return returnValue; };
        var continuedValue = string.Empty;

        TryCatch.Execute(action)
            .ContinueWith(value => continuedValue = returnValue)
            .Finalize();

        succeeded.Should().BeTrue();
        continuedValue.Should().Be(returnValue);
    }


    [Fact]
    public async Task TryCatch_ContinueWithAsync_ShouldContinueWhenActionSucceeds_WithoutResult()
    {
        var succeeded = false;
        var action = () => { succeeded = true; };
        var continued = false;

        await TryCatch.Execute(action)
            .ContinueWithAsync(async _ =>
            {
                await Task.Factory.StartNew(() => continued = true);
            })
            .FinalizeAsync();

        succeeded.Should().BeTrue();
        continued.Should().BeTrue();
    }

    [Fact]
    public async Task TryCatch_ContinueWithAsync_ShouldNotContinueWhenActionThrows_WithoutResult()
    {
        var succeeded = false;
        var exception = new InvalidOperationException();
        var action = () => { succeeded = true; throw exception; };
        var continued = false;

        await TryCatch.Execute(action)
            .ContinueWithAsync(async _ =>
            {
                await Task.Factory.StartNew(() => continued = true);
            })
            .FinalizeAsync();

        succeeded.Should().BeTrue();
        continued.Should().BeFalse();
    }

    [Fact]
    public async Task TryCatch_ContinueWithAsync_ShouldContinueWhenActionSucceeds_WithResult()
    {
        var returnValue = "Hello world";
        var succeeded = false;
        var action = () => { succeeded = true; return returnValue; };
        var continuedValue = string.Empty;

        await TryCatch.Execute(action)
            .ContinueWithAsync(async value =>
            {
                await Task.Factory.StartNew(() => continuedValue = returnValue);
            })
            .FinalizeAsync();

        succeeded.Should().BeTrue();
        continuedValue.Should().Be(returnValue);
    }
}
