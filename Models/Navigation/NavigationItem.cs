namespace Taggle.Models.Navigation;

public sealed class NavigationItem(string name, Symbol icon, NavigationTarget? target = null, Symbol? activeIcon = null)
{
	/// <summary>
	/// Name of the navigation item
	/// </summary>
	public string Name { get; set; } = name;

	/// <summary>
	/// The current icon being displayed
	/// </summary>
	public Symbol Icon { get; set; } = icon;

	/// <summary>
	/// Any sub-items displayed
	/// </summary>
	public ObservableCollection<NavigationItem> Children { get; set; } = [];

	/// <summary>
	/// The default / inactive icon
	/// </summary>
	public Symbol DefaultIcon { get; set; } = icon;

	/// <summary>
	/// The icon being displayed if the item is active
	/// </summary>
	public Symbol? ActiveIcon { get; set; } = activeIcon;

	/// <summary>
	/// Binding to check if the item can be invoked
	/// </summary>
	public bool SelectOnInvoke => Target is not null;

	/// <summary>
	/// The target service, if not set, will be suppressed
	/// </summary>
	public NavigationTarget? Target { get; set; } = target;
}
