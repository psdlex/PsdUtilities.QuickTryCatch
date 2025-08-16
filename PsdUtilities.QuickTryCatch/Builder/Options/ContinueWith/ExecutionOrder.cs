namespace PsdUtilities.QuickTryCatch.Builder.Options.ContinueWith;

public sealed class ExecutionOrder
{
    private ExecutionOrder(ExecutionOrderDefinition definition, int orderValue)
    {
        Definition = definition;
        OrderValue = orderValue;
    }

    public ExecutionOrderDefinition Definition { get; }
    public int OrderValue { get; }

    public static readonly ExecutionOrder First = new(ExecutionOrderDefinition.First, int.MinValue);
    public static readonly ExecutionOrder Last = new(ExecutionOrderDefinition.Last, int.MaxValue);

    public static ExecutionOrder FromValue(int ascendingOrder) => new(ExecutionOrderDefinition.ValueBased, ascendingOrder);
}