using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entity;

public class EmployeeAccount : IdentityUser<long>
{
    public long EmployeeId { get; set; }
}