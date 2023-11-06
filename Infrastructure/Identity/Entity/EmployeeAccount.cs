using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entity;

public class EmployeeAccount : IdentityUser<long>
{
    public long EmployeeId { get; set; }
}