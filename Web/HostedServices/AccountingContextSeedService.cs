using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.HostedServices;

public class AccountingContextSeedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AccountingContextSeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();

        AccountingSystemContext context = scope.ServiceProvider.GetRequiredService<AccountingSystemContext>();

        if (!context.Database.IsInMemory())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }

        await AccountingSystemContextSeed.SeedAsync(context);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}