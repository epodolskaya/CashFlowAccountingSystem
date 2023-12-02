using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityTypeConfigurations;

public class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
{
    public void Configure(EntityTypeBuilder<OperationType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.OperationCategories).WithOne(x => x.Type).HasForeignKey(x => x.TypeId);
    }
}