namespace Taggle.Models.Navigation;

public sealed class NavigationItem
{
	public required string Name { get; set; }
	public required Symbol CurrentIcon { get; set; }
	public required Symbol DefaultIcon { get; set; }
	public Symbol? ActiveIcon { get; set; }
	public Type? Target { get; set; } // The target service, if not set, will be suppressed
}
