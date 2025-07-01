namespace Taggle.ViewModels.Frames;

[RegisterSingleton]
public sealed partial class SettingsFrameViewModel(ConfigController configController) : ViewModelBase
{
	#region THEME
	/// <summary>
	/// Current selected theme Id (string)
	/// </summary>
	[ObservableProperty]
	private string _selectedTheme = configController.Config.Theme;

	/// <summary>
	/// List of available themes
	/// </summary>
	public string[] AppThemes { get;  } = [
		ThemeHelper.THEME_SYSTEM_ID,
		ThemeHelper.THEME_LIGHT_ID,
		ThemeHelper.THEME_DARK_ID];

	/// <summary>
	/// If the theme id changes, applies the corresponding theme
	/// </summary>
	partial void OnSelectedThemeChanged(string value)
	{
		if (configController.Config.Theme == value) return;
		configController.Config.Theme = value;
		configController.SaveConfigToFile();
	}
	#endregion

	#region APP DATA
	/// <summary>
	/// Opens the app-data directory for taggle
	/// </summary>
	[RelayCommand]
	public void OpenAppDataFolder() =>
		ExplorerHelper.OpenFolder(AppDataService.AppDataDirectory);

	#endregion
}
