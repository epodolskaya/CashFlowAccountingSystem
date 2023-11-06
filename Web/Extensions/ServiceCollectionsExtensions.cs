using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Reflection;
using Web.HostedServices;

namespace Web.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue("UseInMemoryDatabase", defaultValue: false))
        {
            services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<DbContext, AccountingSystemContext>
                        (options =>
                        {
                            options.UseInMemoryDatabase("AccountingSystemDatabase");
                        });
        }
        else
        {
            services.AddEntityFrameworkMySql()
                    .AddDbContext<DbContext, AccountingSystemContext>
                        (options =>
                        {
                            string connectionString = configuration.GetConnectionString("AccountingSystemContext")!;
                            options.UseMySql
                                (new MySqlConnection(connectionString),
                                 ServerVersion.AutoDetect(connectionString),
                                 sqlOptions =>
                                 {
                                     sqlOptions.MigrationsAssembly(typeof(AccountingSystemContext).GetTypeInfo().Assembly.GetName().Name);
                                     sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), errorNumbersToAdd: null);
                                 });

                            options.UseSnakeCaseNamingConvention();
                        });
        }
    }

    public static void AddAndConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<AccountingContextSeedService>();
    }
}