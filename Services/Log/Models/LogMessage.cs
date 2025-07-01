namespace Taggle.Services.Log.Models;

/// <summary>
/// Defines a log message with title, message and severity
/// </summary>
public readonly record struct LogMessage(string Title, string Message, LogSeverity Severity = LogSeverity.Informational)
{
	public string Title { get; } = Title;
	public string Message { get; } = Message;

	public LogSeverity Severity { get; } = Severity;
	public Guid Id { get; } = Guid.NewGuid();
}
