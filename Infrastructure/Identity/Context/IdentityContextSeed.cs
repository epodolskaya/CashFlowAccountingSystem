using Infrastructure.Data;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Context;

public class IdentityContextSeed
{
    public static async Task SeedAsync(UserManager<EmployeeAccount> userManager,
                                       RoleManager<IdentityRole<long>> roleManager,
                                       AccountingSystemContext repository)
    {
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.DepartmentHead));
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.DepartmentEmployee));

        EmployeeAccount departmentHead = new EmployeeAccount
        {
            UserName = "head@gmail.com",
            Email = "head@gmail.com",
            EmployeeId = (await repository.Employees.SingleAsync(x => x.Name == "Елизавета" && x.Surname == "Подольская")).Id
        };

        await userManager.CreateAsync(departmentHead, "P@ssword1");
        EmployeeAccount? createdHead = await userManager.FindByEmailAsync("head@gmail.com");

        if (createdHead != null)
        {
            await userManager.AddToRoleAsync(createdHead, RoleName.DepartmentHead);
        }
    }
}