namespace Taggle.Services.Log.Controllers;

[RegisterSingleton]
public sealed class LogController
{
	private readonly bool _debugMode;
	private readonly string _logFileName = $"{DateTime.UtcNow:yyyy-MM-dd HH-mm-ss}.log";
	private readonly string _logDirectory = Path.Combine(Path.GetTempPath() + AppDomain.CurrentDomain.FriendlyName);

	public string LogFilePath => Path.Combine(_logDirectory, _logFileName);

	public readonly ObservableCollection<LogMessage> Logs = [];

	public LogController()
	{
		#if DEBUG
		_debugMode = true;
		#endif
	}

	/// <summary>
	/// Ensures that the log directory and the log file of the current
	/// instance exists.
	/// </summary>
	/// <returns></returns>
	public async Task<bool> EnsureLogFile()
	{
		if (!(Directory.Exists(_logDirectory) || Directory.CreateDirectory(_logDirectory).Exists))
			return false;
		await File.AppendAllTextAsync(LogFilePath, string.Empty);
		return true;
	}

	#region INFO / WARN / ERROR / DEBUG
	/// <summary>
	/// Logs an informational message
	/// </summary>
	public void Info(string message,
		[CallerMemberName] string memberName = "",
		[CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal($"{className} - {memberName}", message, LogSeverity.Informational);
	}

	/// <summary>
	/// Logs a warning message
	/// </summary>
	public void Warn(string message,
		[CallerMemberName] string memberName = "",
		[CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal($"{className} - {memberName}", message, LogSeverity.Warning);
	}

	/// <summary>
	/// Logs an exception with calling method information
	/// </summary>
	public void Exception(Exception ex,
		[CallerMemberName] string memberName = "",
		[CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal(className, $"Method: {memberName} - {ex.Message}", LogSeverity.Error);
	}

	/// <summary>
	/// Logs an error message
	/// </summary>
	public void Error(string message,
		[CallerMemberName] string memberName = "",
		[CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal($"{className} - {memberName}", message, LogSeverity.Error);
	}

	/// <summary>
	/// Logs a debug message
	/// </summary>
	public void Debug(string message, [CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal(className, message, LogSeverity.Debug);
	}

	/// <summary>
	/// Logs a success message
	/// </summary>
	public void Success(string message, [CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal(className, message, LogSeverity.Success);
	}
	#endregion

	#region LOG
	/// <summary>
	/// Internal method to handle logging logic
	/// </summary>
	private void LogInternal(string title, string message, LogSeverity severity)
	{
		if (_debugMode) Console.WriteLine(message);
		if (severity == LogSeverity.Debug) return;

		var logMessage = new LogMessage(title, message, severity);
		Logs.Add(logMessage);

		File.AppendAllText(LogFilePath, $"{DateTime.UtcNow} - {severity} - {title} - {message}{Environment.NewLine}");
	}

	/// <summary>
	/// Removes the log with the given id from the list of logs to display
	/// </summary>
	public void RemoveLog(Guid id) =>
		Logs.Remove(Logs.First(x => x.Id == id));

	/// <summary>
	/// Clears all available logs
	/// </summary>
	public void ClearLogs() =>
		Logs.Clear();
	#endregion

}
