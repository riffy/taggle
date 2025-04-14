namespace Taggle.ViewModels;

[RegisterSingleton]
public partial class MainWindowViewModel : ViewModelBase
{
	private readonly IMessenger _messenger = WeakReferenceMessenger.Default;

	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public MainWindowViewModel(IServiceProvider sP)
	{
		_currentPage = sP.GetRequiredService<SplashScreenViewModel>();
		_messenger.Register<MainWindowViewModel, RouteMessage>(this, (_, route) =>
		{
			var page = (ViewModelBase?)sP.GetRequiredService(route.Value);
			if (page is null) return;
			CurrentPage = page;
		});
	}
}
