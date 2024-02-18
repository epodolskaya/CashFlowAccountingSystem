using DomainServices.Behaviors;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Identity.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

namespace DomainServices.Tests;

static internal class DependencyContainer
{
    public static IServiceProvider GetServiceProvider()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();

        serviceCollection.AddEntityFrameworkInMemoryDatabase()
                         .AddDbContext<DbContext, AccountingSystemContext>
                             (options =>
                             {
                                 options.UseInMemoryDatabase("AccountingSystemDatabase");
                             });

        serviceCollection.AddEntityFrameworkInMemoryDatabase()
                         .AddDbContext<IdentityContext>
                             (options =>
                             {
                                 options.UseInMemoryDatabase("IdentityContextDatabase");
                             });

        serviceCollection.AddValidatorsFromAssembly
            (typeof(ValidationBehaviour<,>).Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        serviceCollection.AddValidatorsFromAssembly
            (typeof(IdentityContext).Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        serviceCollection.AddValidatorsFromAssembly
            (typeof(Program).Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        serviceCollection.AddMediatR
            (options =>
            {
                options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
                options.RegisterServicesFromAssembly(typeof(IdentityContext).Assembly);
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();

        AccountingSystemContextSeed.SeedAsync(scope.ServiceProvider.GetService<AccountingSystemContext>()).Wait();

        return serviceProvider;
    }
}