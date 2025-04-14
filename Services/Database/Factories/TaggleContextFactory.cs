namespace Taggle.Services.Database.Factories;

public sealed class TaggleContextFactory(DbContextOptions<TaggleContext> options) : IDbContextFactory<TaggleContext>
{
	public TaggleContext CreateDbContext() => new(options);
}
