namespace Taggle.Services.Database.Services;

[RegisterSingleton]
public sealed class DatabaseService(IDbContextFactory<TaggleContext> dbContextFactory)
{
	/// <summary>
	/// Ensures the local database exists.
	/// </summary>
	/// <returns></returns>
	public async Task<bool> EnsureDatabase()
	{
		await using var dbContext = await dbContextFactory.CreateDbContextAsync();
		await dbContext.Database.MigrateAsync();
		return true;
	}
}
