namespace Taggle;

public partial class App : Application
{
	public IServiceProvider? Services { get; private set; }
	public static List<string> StartupArguments { get; private set; } = [];

	public static Window? MainWindow { get; private set; }

    public override void Initialize() =>
	    AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
	    if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

	    // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
	    // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
	    DisableAvaloniaDataAnnotationValidation();

	    // Save the startup arguments
	    if (desktop.Args is not null) StartupArguments = desktop.Args.ToList();

	    // Register all services
		Services = new ServiceCollection()
			.AddOptions()
			.AddDbContextFactory<TaggleContext, TaggleContextFactory>(options =>
			{
				#if DEBUG
					options
						.UseSqlite($"Data Source=Taggle.db")
						.EnableSensitiveDataLogging()
						.EnableDetailedErrors()
						.LogTo(Console.WriteLine, LogLevel.Information);
				#else
					options
						.UseSqlite($"Data Source={Path.Combine(AppDataService.AppDataDirectory, "Taggle.db")}")
						.EnableDetailedErrors();
				#endif
			})
			.AutoRegister()
			.BuildServiceProvider();

		MainWindow = desktop.MainWindow = new MainWindow
		{
			DataContext = Services.GetRequiredService<MainWindowViewModel>()
		};

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
            BindingPlugins.DataValidators.Remove(plugin);
    }

    #region THEME

    /// <summary>
    /// Toggles the dark mode on/off based on the current
    /// theme variant.
    /// </summary>
    public static void ToggleDarkMode()
    {
	    if (MainWindow is null) return;
	    MainWindow.RequestedThemeVariant =
		    MainWindow.RequestedThemeVariant == ThemeVariant.Light ?
			    ThemeVariant.Dark : ThemeVariant.Light;
    }

    #endregion
}
