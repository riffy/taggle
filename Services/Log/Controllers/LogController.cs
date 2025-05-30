﻿namespace Taggle.Services.Log.Controllers;

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

	#region LOG

	/// <summary>
	/// Logs the message with the severity and the title
	/// </summary>
	public void Log(string title, string message, InfoBarSeverity severity = InfoBarSeverity.Informational) =>
		LogInternal(title, message, severity);

	/// <summary>
	/// Logs the message with the severity and the title is the class name of the caller
	/// </summary>
	public void Log(string message, InfoBarSeverity severity = InfoBarSeverity.Informational, [CallerFilePath] string filePath = "")
	{
		var className = Path.GetFileNameWithoutExtension(filePath);
		LogInternal(className, message, severity);
	}

	/// <summary>
	/// Internal method to handle logging logic
	/// </summary>
	private void LogInternal(string title, string message, InfoBarSeverity severity)
	{
		if (_debugMode) Console.WriteLine(message);

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
