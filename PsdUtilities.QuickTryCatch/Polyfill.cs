using System.ComponentModel;

namespace System.Runtime.CompilerServices;

[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IsExternalInit { }

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
internal sealed class RequiredMemberAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal sealed class CompilerFeatureRequiredAttribute : Attribute
{
    public CompilerFeatureRequiredAttribute(string featureName) => FeatureName = featureName;
    public string FeatureName { get; }
    public bool IsOptional { get; init; }
    public const string RefStructs = nameof(RefStructs);
    public const string RequiredMembers = nameof(RequiredMembers);
}