using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public sealed class CatchAllTests
{
    [Fact]
    public void TryCatch_CatchAll_ShouldCatchAnyException()
    {
        var exception = Utilities.GetRandomException();
        var action = () => { throw exception; };
        Exception? caughtException = null;

        TryCatch.Execute(action)
            .CatchAll(ex => caughtException = ex)
            .Finalize();

        caughtException?.GetType().Should().Be(exception.GetType());
    }

    [Fact]
    public async Task TryCatch_CatchAllAsync_ShouldCatchAnyException()
    {
        var exception = Utilities.GetRandomException();
        var action = () => { throw exception; };
        Exception? caughtException = null;

        await TryCatch.Execute(action)
            .CatchAllAsync(async ex =>
            {
                await Task.Factory.StartNew(() => caughtException = ex);
            })
            .FinalizeAsync();

        caughtException?.GetType().Should().Be(exception.GetType());
    }
}
