namespace Taggle.Services.Database.Factories;

/// <summary>
/// Database context factory for Taggle
/// </summary>
public sealed class TaggleContextFactory(DbContextOptions<TaggleContext> options) : IDbContextFactory<TaggleContext>
{
	public TaggleContext CreateDbContext() => new(options);
}
