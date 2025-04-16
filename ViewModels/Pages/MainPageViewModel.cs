namespace Taggle.ViewModels.Pages;

[RegisterSingleton]
public sealed partial class MainPageViewModel : ViewModelBase
{
	[ObservableProperty]
	private string _title = string.Empty;

	public MainPageViewModel()
	{
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

	private void LoadNavigationItems()
	{
		NavigationItems.Add(new()
		{
			CurrentIcon = Symbol.Home,
			DefaultIcon = Symbol.Home,
			ActiveIcon = Symbol.HomeFilled,
			Name = "Home"
		});
		NavigationItems.Add(new()
		{
			CurrentIcon = Symbol.Zoom,
			DefaultIcon = Symbol.Zoom,
			ActiveIcon = Symbol.Zoom,
			Name = "Search"
		});
	}
	#endregion

	#region FRAME
	[ObservableProperty]
	private ViewModelBase? _currentFrame;

	#endregion
}
