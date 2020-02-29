namespace System.Diagnostics.CodeAnalysis {
    // https://github.com/dotnet/roslyn/issues/37544#issuecomment-533639747
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class NotNullAttribute : Attribute {}
}