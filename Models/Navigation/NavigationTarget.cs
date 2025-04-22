namespace Taggle.Models.Navigation;

/// <summary>
/// A Navigation target constructed by the viewmodel and possible args
/// </summary>
/// <param name="targetType">The ViewModel service (singleton / transient) to navigate to</param>
/// <param name="args">Possible arguments while creating an instance</param>
public sealed class NavigationTarget(Type targetType, object[]? args = null)
{
	public Type TargetType { get; } = targetType;
	public object[] Args { get; init; } = args ?? [];
}
