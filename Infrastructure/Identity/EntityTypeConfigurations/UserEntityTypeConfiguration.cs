using Infrastructure.Identity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EntityTypeConfigurations;
internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeAccount>
{
    public void Configure(EntityTypeBuilder<EmployeeAccount> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
