namespace Taggle.Services.Config.Models;

public sealed class LocalConfig : INotifyPropertyChanged
{
	#region THEME
	/// <summary>
	/// The saved theme of the app
	/// </summary>
	private string _theme = ThemeHelper.THEME_SYSTEM_ID;

	public string Theme
	{
		get => _theme;
		set
		{
			_theme = value;
			OnPropertyChanged();
		}
	}

	#endregion

	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
		PropertyChanged?.Invoke(this, new (propertyName));
}
