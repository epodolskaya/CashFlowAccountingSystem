using Infrastructure.Identity.Entity;
using Infrastructure.Identity.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Context;

public class IdentityContext : IdentityDbContext<EmployeeAccount, IdentityRole<long>, long>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        base.OnModelCreating(builder);
    }
}