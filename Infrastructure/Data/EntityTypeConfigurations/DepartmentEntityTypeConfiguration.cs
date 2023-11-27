using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityTypeConfigurations;
internal class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.Operations).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);
        builder.HasMany(x => x.Employees).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);
        builder.HasMany(x => x.OperationCategories).WithMany(x => x.Departments);
    }
}
