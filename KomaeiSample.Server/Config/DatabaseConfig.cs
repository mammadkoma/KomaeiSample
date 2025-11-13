namespace KomaeiSample.Server.Config;
public static class DatabaseConfig
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            // SQLExpress2022
            options.UseSqlServer(connectionString);

            // localdb
            //options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BimeKojaSms;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            // Liara
            //options.UseSqlServer("Data Source=bimekojasms,1433;Initial Catalog=myDB;User Id=sa;Password=8okYp3GHvbeX8nLA96G7Xu2f;");
        });
        return services;
    }
}