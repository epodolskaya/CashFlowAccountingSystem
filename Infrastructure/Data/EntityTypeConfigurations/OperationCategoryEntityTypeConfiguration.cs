using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityTypeConfigurations;
internal class OperationCategoryEntityTypeConfiguration : IEntityTypeConfiguration<OperationCategory>
{
    public void Configure(EntityTypeBuilder<OperationCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).IsRequired();
        builder.HasMany(x => x.Operations).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
    }
}