using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityTypeConfigurations;
internal class OperationEntityTypeConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Comment).IsRequired(false);
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Sum).IsRequired();
        builder.HasOne(x => x.Category).WithMany(x => x.Operations).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Type).WithMany(x => x.Operations).HasForeignKey(x => x.TypeId);
    }
}