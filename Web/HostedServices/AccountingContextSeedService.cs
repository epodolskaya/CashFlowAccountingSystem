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

        AccountingSystemContext shopContext = scope.ServiceProvider.GetRequiredService<AccountingSystemContext>();

        if (!shopContext.Database.IsInMemory())
        {
            await shopContext.Database.MigrateAsync(cancellationToken);
        }

        await AccountingSystemContextSeed.SeedAsync(shopContext);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}