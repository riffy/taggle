namespace Taggle.Services.Config.Services;

[RegisterSingleton]
public sealed class ConfigService(LogController logController)
{

	private readonly string _configPath = Path.Combine(AppDataService.AppDataDirectory, "config.json");

	#region DIRECTORY / FILE

	/// <summary>
	/// Saves the current local config to the file in %appdata%
	/// </summary>
	public async Task<bool> SaveConfigToFile(LocalConfig config)
	{
		try
		{
			await File.WriteAllTextAsync(_configPath, JsonSerializer.Serialize(config, ConfigOptions.JsonSerializerOptions));
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}

	/// <summary>
	/// Loads the local config file from %appdata% and returns it.
	/// Returns null on error.
	/// </summary>
	public async Task<LocalConfig?> LoadConfigFromFile()
	{
		try
		{
			if (!File.Exists(_configPath)) return null;

			var configJson = await File.ReadAllTextAsync(_configPath);
			var config = JsonSerializer.Deserialize<LocalConfig>(configJson, ConfigOptions.JsonSerializerOptions);
			return config;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return null;
		}
	}
	#endregion
}
