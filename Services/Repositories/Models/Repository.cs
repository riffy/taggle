namespace Taggle.Services.Repositories.Models;

[Table("repository")]
public sealed class Repository
{
	[Key]
	public uint Id { get; set; }

	/// <summary>
	/// The path to the folder for the repository
	/// </summary>
	public string Path { get; set; } = string.Empty;
}
