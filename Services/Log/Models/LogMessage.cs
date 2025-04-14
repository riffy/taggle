namespace Taggle.Services.Log.Models;

public readonly record struct LogMessage(string Title, string Message, InfoBarSeverity Severity = InfoBarSeverity.Informational)
{
	public string Title { get; } = Title;
	public string Message { get; } = Message;

	public InfoBarSeverity Severity { get; } = Severity;
	public Guid Id { get; } = Guid.NewGuid();
}
