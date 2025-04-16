namespace Taggle.Services.Config.Models;

public sealed class LocalConfig : INotifyPropertyChanged
{
	#region DARK MODE
	private bool _darkMode;

	public bool DarkMode
	{
		get => _darkMode;
		set
		{
			_darkMode = value;
			OnPropertyChanged();
		}
	}
	#endregion

	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
		PropertyChanged?.Invoke(this, new (propertyName));
}
