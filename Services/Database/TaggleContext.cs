namespace Taggle.Services.Database;

public sealed class TaggleContext(DbContextOptions<TaggleContext> options) : DbContext(options)
{
}
