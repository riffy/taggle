namespace Taggle.ViewModels;

[RegisterSingleton]
public partial class MainWindowViewModel : ViewModelBase
{
	private readonly IMessenger _messenger = WeakReferenceMessenger.Default;
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;

	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public MainWindowViewModel(IServiceProvider sP, LogController lc)
	{
		_serviceProvider = sP;
		_logController = lc;
		_messenger.Register<MainWindowViewModel, RouteMessage>(this, RouteToPage);
		_currentPage = sP.GetRequiredService<SplashScreenPageViewModel>();
	}

	/// <summary>
	/// Receiver for the RouteMessage, tries to route to the given page / service.
	/// </summary>
	private void RouteToPage(MainWindowViewModel recipient, RouteMessage message)
	{
		try
		{
			var page = (ViewModelBase?)_serviceProvider.GetService(message.Value);
			if (page is null)
			{
				_logController.Log($"No page / service found for {message.Value.ToString()}", InfoBarSeverity.Error);
				return;
			}

			CurrentPage = page;
		}
		catch (Exception ex)
		{
			_logController.Log($"{nameof(RouteToPage)} - Exception occurred: {ex.Message}", InfoBarSeverity.Error);
		}
	}
}
