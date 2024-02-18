using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Services;
using Infrastructure.Interfaces;
using Infrastructure.Tests.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Infrastructure.Tests;

static internal class DependencyContainer
{
    public static IServiceProvider GetServiceProvider()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();

        serviceCollection.AddOptions<JwtSettings>()
                         .Configure
                             (x =>
                             {
                                 x.Audience = "TestTestTestTestTestTestTestTestTestTest";
                                 x.Issuer = "TestTestTestTestTestTestTestTestTestTestTest";
                                 x.SecretKey = "TestTestTestTestTestTestTestTestTestTestTest";
                                 x.TokenLifetimeMinutes = 120;
                             });

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
            (typeof(IdentityContext).Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        serviceCollection.AddValidatorsFromAssembly
            (typeof(Program).Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        serviceCollection.AddMediatR
            (options =>
            {
                options.RegisterServicesFromAssembly(typeof(IdentityContext).Assembly);
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

        serviceCollection.AddIdentity<EmployeeAccount, IdentityRole<long>>()
                         .AddEntityFrameworkStores<IdentityContext>()
                         .AddDefaultTokenProviders();

        serviceCollection.AddScoped<IAuthorizationService, AuthorizationService>();

        serviceCollection.AddScoped<ITokenClaimsService, IdentityTokenClaimsService>();

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();

        AccountingSystemContextSeed.SeedAsync(scope.ServiceProvider.GetService<AccountingSystemContext>()!).Wait();

        UserManager<EmployeeAccount> userManager = scope.ServiceProvider.GetService<UserManager<EmployeeAccount>>()!;
        RoleManager<IdentityRole<long>> roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<long>>>()!;

        IdentityContextSeed.SeedAsync(userManager, roleManager).Wait();

        return serviceProvider;
    }
}