namespace Taggle.Services.Database;

public sealed class TaggleContextFactory(DbContextOptions<TaggleContext> options) : IDbContextFactory<TaggleContext>
{
	public TaggleContext CreateDbContext() => new(options);
}
