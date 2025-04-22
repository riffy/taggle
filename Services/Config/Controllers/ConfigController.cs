namespace Taggle.Services.Config.Controllers;

[RegisterSingleton]
public sealed class ConfigController(LogController logController, ConfigService configService)
{
	public readonly LocalConfig Config = new();

	/// <summary>
	/// Saves the current local config to the temp file
	/// </summary>
	/// <returns></returns>
	public async Task<bool> SaveConfigToFile() =>
		await configService.SaveConfigToFile(Config);

	/// <summary>
	/// Loads the local config file and saves into
	/// the Config property.
	/// If set and the config file doesn't exist, a new one is created.
	/// Returns true on success, else false
	/// </summary>
	/// <returns></returns>
	public async Task<bool> InitializeConfig()
	{
		try
		{
			var config = await configService.LoadConfigFromFile();
			if (config is null)
				return await SaveConfigToFile();
			// Initialize fields
			Config.Theme = config.Theme;
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}
}
