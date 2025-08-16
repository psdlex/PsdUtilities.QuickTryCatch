using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class ExecuteTests
{
    [Fact]
    public void TryCatch_Execute_ShouldExecute()
    {
        var succeeds = false;
        var action = () => succeeds = true;

        TryCatch.Execute(action).Finalize();

        succeeds.Should().BeTrue();
    }

    [Fact]
    public void TryCatch_Execute_ShouldExecuteAndFail()
    {
        var succeeds = false;
        var exception = new ArgumentException();
        var action = () => { throw exception; };

        TryCatch.Execute(action).Finalize();

        succeeds.Should().BeFalse();
    }

    [Fact]
    public async Task TryCatch_ExecuteAsync_ShouldExecute()
    {
        var succeeds = false;
        var action = () => 
        {
            succeeds = true;
            return Task.CompletedTask;
        };

        await TryCatch.ExecuteAsync(action).FinalizeAsync();

        succeeds.Should().BeTrue();
    }

    [Fact]
    public async Task TryCatch_ExecuteAsync_ShouldExecuteAndFail()
    {
        var succeeds = false;
        var exception = new ArgumentException();
        Func<Task> action = () =>
        {
            throw exception;
        };

        await TryCatch.ExecuteAsync(action).FinalizeAsync();

        succeeds.Should().BeFalse();
    }
}
