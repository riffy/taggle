namespace Taggle.Services.Config.Controllers;

[RegisterSingleton]
public sealed class ConfigController(LogController logController, ConfigService configService)
{
	public LocalConfig Config { get; private set; } = new();

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
	public async Task<bool> LoadConfigFromFile(bool createIfMissing)
	{
		try
		{
			var config = await configService.LoadConfigFromFile();
			if (config is null)
			{
				if (createIfMissing) return await SaveConfigToFile();
				return false;
			}
			Config = config;
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}
}
