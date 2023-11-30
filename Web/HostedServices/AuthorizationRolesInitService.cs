using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.HostedServices;

public class AuthorizationRolesInitService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AuthorizationRolesInitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();

        UserManager<EmployeeAccount> userManager = scope.ServiceProvider.GetRequiredService<UserManager<EmployeeAccount>>();

        RoleManager<IdentityRole<long>>? roleManager = scope.ServiceProvider.GetRequiredService
                                                               (typeof(RoleManager<IdentityRole<long>>))
                                                           as RoleManager<IdentityRole<long>>;

        IdentityContext context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

        if (!context.Database.IsInMemory())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }

        await IdentityContextSeed.SeedAsync(userManager, roleManager);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}