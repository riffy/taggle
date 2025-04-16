namespace Taggle.ViewModels;

[RegisterSingleton]
public partial class MainWindowViewModel : ViewModelBase
{
	private readonly IMessenger _messenger = WeakReferenceMessenger.Default;
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;
	private readonly ConfigController _configController;

	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public MainWindowViewModel(IServiceProvider sP, LogController lc, ConfigController cc)
	{
		_serviceProvider = sP;
		_logController = lc;
		_configController = cc;
		_messenger.Register<MainWindowViewModel, MainWindowRouteMessage>(this, RouteToPage);
		CurrentPage = sP.GetRequiredService<SplashScreenPageViewModel>();
		_configController.Config.PropertyChanged += ApplyDarkMode;
	}

	private void ApplyDarkMode(object? sender, PropertyChangedEventArgs e)
	{
		Console.WriteLine("0");
		if (e.PropertyName != nameof(LocalConfig.DarkMode)) return;
		Console.WriteLine("1");
		if (App.MainWindow is null) return;
		Console.WriteLine("2");
		App.MainWindow.RequestedThemeVariant =
			_configController.Config.DarkMode ? ThemeVariant.Dark : ThemeVariant.Light;
	}

	/// <summary>
	/// Receiver for the MainWindowRouteMessage, tries to route to the given page / service.
	/// </summary>
	private void RouteToPage(MainWindowViewModel recipient, MainWindowRouteMessage message)
	{
		try
		{
			var page = (ViewModelBase?)_serviceProvider.GetRequiredService(message.Value);
			if (page is null)
			{
				_logController.Error($"No page / service found for {message.Value.ToString()}");
				return;
			}
			_logController.Debug($"Switching MainWindow Navigation to: {message.Value.ToString()}");
			CurrentPage = page;
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}
}
