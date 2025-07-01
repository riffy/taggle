namespace Taggle.Services.Repositories.Controllers;

[RegisterSingleton]
public sealed class RepositoryController(IDbContextFactory<TaggleContext> dbContextFactory, LogController logController)
{
	/// <summary>
	/// Reads all repositories from the database
	/// </summary>
	/// <returns></returns>
	public async Task<List<Repository>> GetRepositoriesAsync()
	{
		try
		{
			await using var dbContext = await dbContextFactory.CreateDbContextAsync();
			return await dbContext.Repositories
				.AsNoTracking()
				.ToListAsync();
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return [];
		}
	}
}
