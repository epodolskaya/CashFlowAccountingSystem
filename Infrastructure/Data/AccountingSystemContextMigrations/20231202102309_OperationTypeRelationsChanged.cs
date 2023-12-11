#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.AccountingSystemContextMigrations;

/// <inheritdoc />
public partial class OperationTypeRelationsChanged : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("fk_operations_department_department_id",
             "operations");

        migrationBuilder.DropForeignKey
            ("fk_operations_operation_types_type_id",
             "operations");

        migrationBuilder.DropIndex
            ("ix_operations_type_id",
             "operations");

        migrationBuilder.AddColumn<long>
            ("type_id",
             "operation_categories",
             "bigint",
             nullable: false,
             defaultValue: 0L);

        migrationBuilder.CreateIndex
            ("ix_operation_categories_type_id",
             "operation_categories",
             "type_id");

        migrationBuilder.AddForeignKey
            ("fk_operation_categories_operation_types_type_id",
             "operation_categories",
             "type_id",
             "operation_types",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("fk_operations_departments_department_id",
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
            ("fk_operation_categories_operation_types_type_id",
             "operation_categories");

        migrationBuilder.DropForeignKey
            ("fk_operations_departments_department_id",
             "operations");

        migrationBuilder.DropIndex
            ("ix_operation_categories_type_id",
             "operation_categories");

        migrationBuilder.DropColumn
            ("type_id",
             "operation_categories");

        migrationBuilder.CreateIndex
            ("ix_operations_type_id",
             "operations",
             "type_id");

        migrationBuilder.AddForeignKey
            ("fk_operations_department_department_id",
             "operations",
             "department_id",
             "departments",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("fk_operations_operation_types_type_id",
             "operations",
             "type_id",
             "operation_types",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }
}