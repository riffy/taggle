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
		NavigationItems.Add(new(
			"Repositories",
			Symbol.ZipFolder,
			new(typeof(RepositoriesFrameViewModel)),
			Symbol.ZipFolderFilled));
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
				foreach (var ni in NavigationItems.Where(ni => ni != item && ni.Icon != ni.DefaultIcon))
					ni.Icon = ni.DefaultIcon;
				if (item.ActiveIcon is not null)
				{
					_logController.Debug($"Active icon navigation to {item.Name}");
					item.Icon = (Symbol)item.ActiveIcon;
				}

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

	#region NOTIFICATIONS
	[ObservableProperty]
	private bool _hasUnreadNotifications;

	[ObservableProperty]
	private Symbol _notificationIcon = Symbol.Alert;

	[ObservableProperty]
	private uint _unreadNotificationsCount;

	#endregion
}
