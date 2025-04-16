namespace Taggle.ViewModels.Pages;

[RegisterSingleton]
public sealed partial class MainPageViewModel : ViewModelBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;
	[ObservableProperty]
	private string _title = string.Empty;

	public MainPageViewModel(IServiceProvider sp, LogController lc)
	{
		_serviceProvider = sp;
		_logController = lc;
		RegisterRouter();
		LoadNavigationItems();
	}

	#region ROUTER
	private readonly IMessenger _messenger = WeakReferenceMessenger.Default;
	private void RegisterRouter()
	{
		_messenger.Register<MainPageViewModel, NavigationItem>(this, (_, message) =>
		{
			Console.WriteLine(message);
		});
	}

	#endregion

	#region NAVIGATION
	[ObservableProperty]
	private ObservableCollection<NavigationItem> _navigationItems = [];
	[ObservableProperty]
	private ObservableCollection<NavigationItem> _footerNavigations = [];

	private void LoadNavigationItems()
	{
		NavigationItems.Add(new("Home", Symbol.Home, null, Symbol.HomeFilled));
		NavigationItems.Add(new("Search", Symbol.Zoom));
		FooterNavigations.Add(new(
			"Settings",
			Symbol.Settings,
			new(typeof(SettingsFrameViewModel)),
			Symbol.SettingsFilled));
	}

	public async Task NavigateUsingItem(NavigationItem item)
	{
		try
		{
			if (item.Target is null)
				_logController.Error($"Can't navigate to {item.Name} as target is null.");
			else
			{
				CurrentFrame = (ViewModelBase)_serviceProvider.GetRequiredService(item.Target.TargetType);
				_logController.Debug($"Frame navigation to {item.Name}");
			}
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}
	#endregion

	#region FRAME
	[ObservableProperty]
	private ViewModelBase? _currentFrame;

	#endregion
}
