namespace Taggle.Services.Database.Factories;

/// <summary>
/// This class is a factory for creating instances of TaggleContext at design time (e.g. necessary for migrations)
/// </summary>
public sealed class TaggleDesignTimeFactory : IDesignTimeDbContextFactory<TaggleContext>
{
	public TaggleContext CreateDbContext(string[] args)
	{
		var options = new DbContextOptionsBuilder<TaggleContext>();
#if DEBUG
		options
			.UseSqlite($"Data Source=Taggle.db")
			.EnableSensitiveDataLogging()
			.EnableDetailedErrors()
			.LogTo(Console.WriteLine, LogLevel.Information);
#else
		options
			.UseSqlite($"Data Source={Path.Combine(AppDataService.AppDataDirectory, "Taggle.db")}")
			.EnableDetailedErrors();
#endif
		Console.WriteLine(options.Options);

		return new (options.Options);
	}
}
