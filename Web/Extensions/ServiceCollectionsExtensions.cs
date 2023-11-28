using DomainServices.Behaviors;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Services;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Web.HostedServices;

namespace Web.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue("UseInMemoryDatabase", false))
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
                                     sqlOptions.MigrationsAssembly
                                         (typeof(AccountingSystemContext).GetTypeInfo().Assembly.GetName().Name);

                                     sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                                 });

                            options.UseSnakeCaseNamingConvention();
                        });
        }
    }

    public static void AddAndConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<AccountingContextSeedService>();
        services.AddHostedService<AuthorizationRolesInitService>();
    }

    public static void AddAndConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization
            (options =>
            {
                options.AddPolicy
                    (PolicyName.FinancialAnalyst,
                     builder =>
                     {
                         builder.RequireClaim(ClaimTypes.Role, RoleName.FinancialAnalyst);
                         builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                     });

                options.AddPolicy
                    (PolicyName.DepartmentHead,
                     builder =>
                     {
                         builder.RequireAssertion
                             (x => x.User.HasClaim(ClaimTypes.Role, RoleName.DepartmentHead) ||
                                   x.User.HasClaim(ClaimTypes.Role, RoleName.FinancialAnalyst));

                         builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                     });
            });
    }

    public static void AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSettings jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        services.AddAuthentication()
                .AddJwtBearer
                    (options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidAudience = jwtSettings.Audience,
                            ValidIssuer = jwtSettings.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                        };
                    });

        if (configuration.GetValue("UseInMemoryDatabase", false))
        {
            services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<IdentityContext>
                        (options =>
                        {
                            options.UseInMemoryDatabase("IdentityContextDatabase");
                        });
        }
        else
        {
            services.AddEntityFrameworkMySql()
                    .AddDbContext<IdentityContext>
                        (options =>
                        {
                            string connectionString = configuration.GetConnectionString("IdentityConnection")!;

                            options.UseMySql
                                (new MySqlConnection(connectionString),
                                 ServerVersion.AutoDetect(connectionString),
                                 sqlOptions =>
                                 {
                                     sqlOptions.MigrationsAssembly(typeof(IdentityContext).GetTypeInfo().Assembly.GetName().Name);
                                     sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                                 });

                            options.UseSnakeCaseNamingConvention();
                        });
        }

        services.AddIdentity<EmployeeAccount, IdentityRole<long>>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IAuthorizationService, AuthorizationService>();

        services.AddScoped<ITokenClaimsService, IdentityTokenClaimsService>();
    }

    public static void AddMediatRServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        services.AddValidatorsFromAssembly(typeof(IdentityContext).Assembly);

        services.AddMediatR
            (options =>
            {
                options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
                options.RegisterServicesFromAssembly(typeof(IdentityContext).Assembly);
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    public static void AddAndConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>().BindConfiguration(nameof(JwtSettings));
    }
}