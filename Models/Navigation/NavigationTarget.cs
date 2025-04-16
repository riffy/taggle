namespace Taggle.Models.Navigation;

public sealed class NavigationTarget(Type targetType, object[]? args = null)
{
	public Type TargetType { get; } = targetType;
	public object[] Args { get; init; } = args ?? [];
}
