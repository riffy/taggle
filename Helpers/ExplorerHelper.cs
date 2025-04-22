namespace Taggle.Helpers;

public static class ExplorerHelper
{
	/// <summary>
	/// Tries to open the given folder path
	/// using the OS explorer.
	/// Dont know if this works for non-Windows though :(
	/// </summary>
	public static void OpenFolder(string folderPath)
	{
		if (string.IsNullOrEmpty(folderPath))
			return;

		try
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				Process.Start("explorer.exe", folderPath);
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				Process.Start("open", folderPath);
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				Process.Start("xdg-open", folderPath);
			else
				try
				{
					// Use .NET Core 3.0+ approach
					using Process process = new Process();
					process.StartInfo = new()
					{
						FileName = folderPath,
						UseShellExecute = true
					};
					process.Start();
				}
				catch
				{
					// Last resort fallback
					var url = new Uri(folderPath).AbsoluteUri;
					Process.Start(new ProcessStartInfo
					{
						FileName = url,
						UseShellExecute = true
					});
				}
		}
		catch (Exception ex)
		{
			// Handle or log any exceptions
			Console.WriteLine($"Error opening folder: {ex.Message}");
		}
	}
}
