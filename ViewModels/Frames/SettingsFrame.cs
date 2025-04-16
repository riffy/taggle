namespace Taggle.ViewModels.Frames;

[RegisterSingleton]
public sealed partial class SettingsFrame : ViewModelBase
{
	private readonly ConfigController _configController;

	public SettingsFrame(ConfigController cc)
	{
		_configController = cc;
		_isDarkModeEnabled = false;
	}

	#region DARKMODE

	[ObservableProperty]
	private bool _isDarkModeEnabled;

	#endregion
}
