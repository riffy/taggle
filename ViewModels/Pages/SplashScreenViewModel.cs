namespace Taggle.ViewModels.Pages;

[RegisterSingleton]
public sealed partial class SplashScreenViewModel : ViewModelBase
{
	private readonly AppDataService _appDataService;
	private readonly DatabaseService _databaseService;
	private readonly LogController _logController;

	public SplashScreenViewModel(AppDataService ads, DatabaseService ds, LogController lc)
	{
		_appDataService = ads;
		_databaseService = ds;
		_logController = lc;
		_loadQueue.Enqueue(InitializeApp);
		_loadQueue.Enqueue(InitializeLog);
		_loadQueue.Enqueue(InitializeDatabase);
		TriggerNextLoadStep();
	}

	#region LOAD QUEUE
	private readonly Queue<Func<Task<bool>>> _loadQueue = new();

	/// <summary>
	/// Triggers the next entry in the load queue.
	/// If the queue is empty, routes the HomePage.
	/// </summary>
	private async Task TriggerNextLoadStep()
	{
		LoadingText = "";
		if (_loadQueue.Count > 0)
		{
			try
			{
				if (await _loadQueue.Dequeue().Invoke())
					TriggerNextLoadStep();
				else
					return;
			}
			catch (Exception ex)
			{
				DisplayInfoBar(
					"Critical Error",
					$"Error while trying to ensure AppData-Directory:\n{ex.Message}",
					InfoBarSeverity.Error);
			}

		}
		else
			DisplayInfoBar("Loading", "Done", InfoBarSeverity.Informational);
	}
	#endregion

	#region LOAD STEPS

	/// <summary>
	/// Initializes the app directory.
	/// </summary>
	private async Task<bool> InitializeApp()
	{
		try
		{
			if (!_appDataService.EnsureAppDataDirectory())
			{
				DisplayInfoBar(
					"Error",
					"Error while trying to ensure AppData-Directory",
					InfoBarSeverity.Error);
				return false;
			}

			return true;
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				"Critical Error",
				$"Critical Error while initializing App:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Initialize log file
	/// </summary>
	private async Task<bool> InitializeLog()
	{
		try
		{
			if (!await _logController.EnsureLogFile())
			{
				DisplayInfoBar(
					"Error",
					"Error while trying to ensure Log-File.",
					InfoBarSeverity.Error);
				return false;
			}
			_logController.Log("Log initialized");
			return true;
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				"Critical Error",
				$"Cirtial Error while initializing Log-File:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Initialize database and apply migrations.
	/// </summary>
	private async Task<bool> InitializeDatabase()
	{
		try
		{
			if (!await _databaseService.EnsureDatabase())
			{
				DisplayInfoBar(
					"Error",
					"Error while trying to ensure Database.",
					InfoBarSeverity.Error);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				"Critical Error",
				$"Cirtial Error while initializing Database:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	#endregion

	#region LOADING TEXT
	[ObservableProperty]
	private string _loadingText = "Test test";
	#endregion

	#region INFO BAR
	[ObservableProperty]
	private bool _showInfoBar;
	[ObservableProperty]
	private string _infoBarTitle = "Yo";
	[ObservableProperty]
	private string _infoBarText = "Message";
	[ObservableProperty]
	private InfoBarSeverity _infoBarSeverity = InfoBarSeverity.Informational;

	/// <summary>
	/// Shows the info bar with the given data.
	/// </summary>
	/// <param name="title">The title of the info</param>
	/// <param name="text">The text of the info</param>
	/// <param name="severity">The severity of the info</param>
	private void DisplayInfoBar(string title, string text, InfoBarSeverity severity)
	{
		ShowInfoBar = true;
		InfoBarTitle = title;
		InfoBarText = text;
		InfoBarSeverity = severity;
	}
	#endregion

	#region INFINITE PROGRESSBAR
	[ObservableProperty]
	private bool _showInfiniteProgress = true;
	#endregion

	#region VALUE PROGRESSBAR
	[ObservableProperty]
	private bool _showValueProgress  = true;
	[ObservableProperty]
	private double _valueProgress;
	[ObservableProperty]
	private double _valueProgressMin;
	[ObservableProperty]
	private double _valueProgressMax = 100;
	#endregion
}
