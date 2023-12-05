using Infrastructure.Identity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.EntityTypeConfigurations;

public class EmployeeAccountEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeAccount>
{
    public void Configure(EntityTypeBuilder<EmployeeAccount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.EmployeeId).IsUnique();
    }
}