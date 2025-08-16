namespace PsdUtilities.QuickTryCatch.Tests.TryCatchTests;

public static class Utilities
{
    private static readonly Type[] _safeExceptions = [
        typeof(ArgumentException),
        typeof(IOException),
        typeof(Exception),
        typeof(InvalidOperationException),
        typeof(NullReferenceException),
        typeof(IndexOutOfRangeException),
        typeof(FormatException)
    ];

    public static Exception GetRandomException()
    {
        var random = new Random(); // different each call
        var type = _safeExceptions[random.Next(_safeExceptions.Length)];
        return (Exception)Activator.CreateInstance(type)!;
    }
}
