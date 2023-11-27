using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.AccountingSystemContextMigrations
{
    /// <inheritdoc />
    public partial class DepartmentEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "department_id",
                table: "operations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "department_id",
                table: "employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "departments_operation_categories",
                columns: table => new
                {
                    departments_id = table.Column<long>(type: "bigint", nullable: false),
                    operation_categories_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments_operation_categories", x => new { x.departments_id, x.operation_categories_id });
                    table.ForeignKey(
                        name: "fk_departments_operation_categories_departments_departments_id",
                        column: x => x.departments_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_departments_operation_categories_operation_categories_operat",
                        column: x => x.operation_categories_id,
                        principalTable: "operation_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_operations_department_id",
                table: "operations",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_department_id",
                table: "employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_departments_operation_categories_operation_categories_id",
                table: "departments_operation_categories",
                column: "operation_categories_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employee_departments_department_id",
                table: "employee",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_operations_department_department_id",
                table: "operations",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employee_departments_department_id",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "fk_operations_department_department_id",
                table: "operations");

            migrationBuilder.DropTable(
                name: "departments_operation_categories");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropIndex(
                name: "ix_operations_department_id",
                table: "operations");

            migrationBuilder.DropIndex(
                name: "ix_employee_department_id",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "operations");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "employee");
        }
    }
}
