namespace PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;

public sealed class ContinueOptionFactory
{
    private int _currentSequence = 0;

    public ContinueOptions CreateDefault()
    {
        return new ContinueOptions
        {
            ExecutionOrder = ExecutionOrder.FromValue(_currentSequence++)
        };
    }
}