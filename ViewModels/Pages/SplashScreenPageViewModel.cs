﻿namespace Taggle.ViewModels.Pages;

[RegisterSingleton]
public sealed partial class SplashScreenPageViewModel : ViewModelBase
{
	private readonly IMessenger _messenger = WeakReferenceMessenger.Default;
	private readonly AppDataService _appDataService;
	private readonly DatabaseService _databaseService;
	private readonly LogController _logController;

	public SplashScreenPageViewModel(AppDataService ads, DatabaseService ds, LogController lc)
	{
		_appDataService = ads;
		_databaseService = ds;
		_logController = lc;
		_loadQueue.Enqueue(InitializeApp);
		_loadQueue.Enqueue(InitializeLog);
		_loadQueue.Enqueue(InitializeDatabase);
		_loadQueue.Enqueue(FinalizeApp);
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
		if (_loadQueue.Count > 0)
			try
			{
				LoadingText = "";
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
		else
			_messenger.Send(new RouteMessage(typeof(MainPageViewModel)));

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
			LoadingText = "Initializing AppData-Directory ...";
			if (_appDataService.EnsureAppDataDirectory()) return true;
			DisplayInfoBar(
				"Error",
				"Error while trying to ensure AppData-Directory",
				InfoBarSeverity.Error);
			return false;

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
			LoadingText = "Initializing Logger ...";
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
			LoadingText = "Creating / Migrating Database ...";
			if (await _databaseService.EnsureDatabase()) return true;
			DisplayInfoBar(
				"Error",
				"Error while trying to ensure Database.",
				InfoBarSeverity.Error);
			return false;
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
			DisplayInfoBar(
				"Critical Error",
				$"Cirtial Error while initializing Database:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Finalizes the app, in case the data is loaded faster
	/// than the UI, queues itself to delay any further UI action.
	/// </summary>
	/// <returns></returns>
	private async Task<bool> FinalizeApp()
	{
		if (App.MainWindow is null)
		{
			_logController.Debug("MainWindow not available yet... Retrying...");
			_loadQueue.Enqueue(FinalizeApp);
			await Task.Delay(500);
		}
		else if (!App.MainWindow.IsLoaded)
		{
			_logController.Debug("MainWindow available, but not loaded yet... Retrying...");
			_loadQueue.Enqueue(FinalizeApp);
			await Task.Delay(500);
		}
		return true;
	}
	#endregion

	#region LOADING TEXT
	[ObservableProperty]
	private string _loadingText = "";
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
	private bool _showValueProgress;
	[ObservableProperty]
	private double _valueProgress;
	[ObservableProperty]
	private double _valueProgressMin;
	[ObservableProperty]
	private double _valueProgressMax = 100;
	#endregion
}
