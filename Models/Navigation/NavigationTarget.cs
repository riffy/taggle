namespace Taggle.Models.Navigation;

public sealed class NavigationTarget
{
	public required Type TargetType { get; init; }
	public required object[] Args { get; init; } = [];
}
