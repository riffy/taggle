namespace Taggle.Services.AppData.Services;

[RegisterSingleton]
public sealed class AppDataService
{
	public static string AppDataDirectory { get; } =
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDomain.CurrentDomain.FriendlyName);

	#region DIRECTORY
	/// <summary>
	/// Checks if the app data directory exists, if not tries to create it.
	/// </summary>
	/// <returns>
	/// <c>True</c> - If the app directory exists or was created
	/// <c>False</c> - On error
	/// </returns>
	public bool EnsureAppDataDirectory() =>
		Directory.Exists(AppDataDirectory) || Directory.CreateDirectory(AppDataDirectory).Exists;

	#endregion
}
