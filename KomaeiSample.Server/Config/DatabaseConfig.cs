namespace KomaeiSample.Server.Config;

public static class DatabaseConfig
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}