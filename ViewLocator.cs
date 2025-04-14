namespace Taggle;

public class ViewLocator : IDataTemplate
{

	/// <summary>
	/// Build the control to display.
	/// </summary>
    public Control? Build(object? param)
    {
	    var name = param?.GetType().FullName;
        if (name is null) return null;

        // Strip ending "ViewModel"
        if (name.EndsWith("ViewModel"))
	        name = name[..^"ViewModel".Length];

        // Replace namespace "ViewModels" with "Views"
        name = name.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
            return (Control)Activator.CreateInstance(type)!;

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data) =>
	    data is ViewModelBase;
}
