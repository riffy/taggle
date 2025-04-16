namespace Taggle.Models.Navigation;

public sealed class NavigationItemTemplateSelector : DataTemplateSelector
{
	[Content]
	public IDataTemplate ItemTemplate { get; set; } = default!;
	public IDataTemplate SeparatorTemplate { get; set; } = default!;
	public IDataTemplate ItemTemplateWithChildren { get; set; } = default!;

	protected override IDataTemplate SelectTemplateCore(object item) =>
		item switch
		{
			Separator => SeparatorTemplate,
			NavigationItem => ItemTemplate,
			_ => ItemTemplateWithChildren
		};
}
