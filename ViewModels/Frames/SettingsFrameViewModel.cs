namespace Taggle.ViewModels.Frames;

[RegisterSingleton]
public sealed partial class SettingsFrameViewModel(ConfigController configController) : ViewModelBase
{
	#region DARKMODE

	[ObservableProperty]
	private bool _isDarkModeEnabled = configController.Config.DarkMode;

	partial void OnIsDarkModeEnabledChanged(bool value)
	{
		if (configController.Config.DarkMode == value) return;
		configController.Config.DarkMode = value;
		configController.SaveConfigToFile();
	}

	#endregion
}
