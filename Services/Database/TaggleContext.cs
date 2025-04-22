namespace Taggle.Services.Database;

/// <summary>
/// The Taggle context, holds all sets necessary for operations.
/// Shall be used to define possible migrations.
/// </summary>
public sealed class TaggleContext(DbContextOptions<TaggleContext> options) : DbContext(options)
{
	public DbSet<Repository> Repositories => Set<Repository>();
}
