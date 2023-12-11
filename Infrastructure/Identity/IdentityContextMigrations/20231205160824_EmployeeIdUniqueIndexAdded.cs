#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Identity.IdentityContextMigrations;

/// <inheritdoc />
public partial class EmployeeIdUniqueIndexAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex
            ("ix_asp_net_users_employee_id",
             "AspNetUsers",
             "employee_id",
             unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex
            ("ix_asp_net_users_employee_id",
             "AspNetUsers");
    }
}