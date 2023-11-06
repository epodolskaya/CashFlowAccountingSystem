using Infrastructure.Data;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Context;

public class IdentityContextSeed
{
    public static async Task SeedAsync(UserManager<EmployeeAccount> userManager,
                                       RoleManager<IdentityRole<long>> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.DepartmentHead));
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.FinancialAnalyst));

        EmployeeAccount financialAnalyst = new EmployeeAccount
        {
            UserName = "analyst@gmail.com",
            Email = "analyst@gmail.com",
            EmployeeId = 1,
        };

        await userManager.CreateAsync(financialAnalyst, "P@ssword1");
        EmployeeAccount? createdUser = await userManager.FindByEmailAsync("analyst@gmail.com");

        if (createdUser != null)
        {
            await userManager.AddToRoleAsync(financialAnalyst, RoleName.FinancialAnalyst);
        }

        EmployeeAccount departmentHead = new EmployeeAccount
        {
            UserName = "head@gmail.com",
            Email = "head@gmail.com",
            EmployeeId = 2,
        };

        await userManager.CreateAsync(departmentHead, "P@ssword1");
        EmployeeAccount? createdHead = await userManager.FindByEmailAsync("head@gmail.com");

        if (createdHead != null)
        {
            await userManager.AddToRoleAsync(createdHead, RoleName.DepartmentHead);
        }
    }
}