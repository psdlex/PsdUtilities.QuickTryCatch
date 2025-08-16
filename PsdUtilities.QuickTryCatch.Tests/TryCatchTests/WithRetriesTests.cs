using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class WithRetriesTests
{
    [Fact]
    public void TryCatch_WithRetries_ShouldRetry3Times()
    {
        var retryCount = 3;
        var retries = -1; // -1 because the initial try is not a RE-try, its just a try
        var exception = new IOException(); // exception is required in order to trigger retry attempts, obviously
        var action = () => { retries++; throw exception; };

        TryCatch.Execute(action)
            .WithRetries(retryCount)
            .Finalize();

        retries.Should().Be(retryCount);
    }
}
