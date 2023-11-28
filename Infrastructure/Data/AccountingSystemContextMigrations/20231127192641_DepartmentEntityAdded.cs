#nullable disable

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.AccountingSystemContextMigrations;

/// <inheritdoc />
public partial class DepartmentEntityAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<long>
            ("department_id",
             "operations",
             "bigint",
             nullable: false,
             defaultValue: 0L);

        migrationBuilder.AddColumn<long>
            ("department_id",
             "employee",
             "bigint",
             nullable: false,
             defaultValue: 0L);

        migrationBuilder.CreateTable
                            ("departments",
                             table => new
                             {
                                 id = table.Column<long>("bigint", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 name = table.Column<string>("longtext", nullable: false)
                                             .Annotation("MySql:CharSet", "utf8mb4")
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_departments", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("departments_operation_categories",
                             table => new
                             {
                                 departments_id = table.Column<long>("bigint", nullable: false),
                                 operation_categories_id = table.Column<long>("bigint", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey
                                     ("pk_departments_operation_categories",
                                      x => new
                                      {
                                          x.departments_id,
                                          x.operation_categories_id
                                      });

                                 table.ForeignKey
                                     ("fk_departments_operation_categories_departments_departments_id",
                                      x => x.departments_id,
                                      "departments",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);

                                 table.ForeignKey
                                     ("fk_departments_operation_categories_operation_categories_operat",
                                      x => x.operation_categories_id,
                                      "operation_categories",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex
            ("ix_operations_department_id",
             "operations",
             "department_id");

        migrationBuilder.CreateIndex
            ("ix_employee_department_id",
             "employee",
             "department_id");

        migrationBuilder.CreateIndex
            ("ix_departments_operation_categories_operation_categories_id",
             "departments_operation_categories",
             "operation_categories_id");

        migrationBuilder.AddForeignKey
            ("fk_employee_departments_department_id",
             "employee",
             "department_id",
             "departments",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("fk_operations_department_department_id",
             "operations",
             "department_id",
             "departments",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("fk_employee_departments_department_id",
             "employee");

        migrationBuilder.DropForeignKey
            ("fk_operations_department_department_id",
             "operations");

        migrationBuilder.DropTable("departments_operation_categories");

        migrationBuilder.DropTable("departments");

        migrationBuilder.DropIndex
            ("ix_operations_department_id",
             "operations");

        migrationBuilder.DropIndex
            ("ix_employee_department_id",
             "employee");

        migrationBuilder.DropColumn
            ("department_id",
             "operations");

        migrationBuilder.DropColumn
            ("department_id",
             "employee");
    }
}