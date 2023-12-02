using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityTypeConfigurations;

public class OperationCategoryEntityTypeConfiguration : IEntityTypeConfiguration<OperationCategory>
{
    public void Configure(EntityTypeBuilder<OperationCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasOne(x => x.Type).WithMany(x => x.OperationCategories).HasForeignKey(x => x.TypeId);
        builder.HasMany(x => x.Operations).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        builder.HasMany(x => x.Departments).WithMany(x => x.OperationCategories).UsingEntity("departments_operation_categories");
    }
}