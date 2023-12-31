﻿using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityTypeConfigurations;

public class OperationEntityTypeConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Comment).IsRequired(false);
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Sum).IsRequired();
        builder.HasOne(x => x.Category).WithMany(x => x.Operations).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Department).WithMany(x => x.Operations).HasForeignKey(x => x.DepartmentId).IsRequired();
    }
}