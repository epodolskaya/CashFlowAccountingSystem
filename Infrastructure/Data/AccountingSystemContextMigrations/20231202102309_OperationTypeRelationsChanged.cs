using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.AccountingSystemContextMigrations
{
    /// <inheritdoc />
    public partial class OperationTypeRelationsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_operations_department_department_id",
                table: "operations");

            migrationBuilder.DropForeignKey(
                name: "fk_operations_operation_types_type_id",
                table: "operations");

            migrationBuilder.DropIndex(
                name: "ix_operations_type_id",
                table: "operations");

            migrationBuilder.AddColumn<long>(
                name: "type_id",
                table: "operation_categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_operation_categories_type_id",
                table: "operation_categories",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_operation_categories_operation_types_type_id",
                table: "operation_categories",
                column: "type_id",
                principalTable: "operation_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_operations_departments_department_id",
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
                name: "fk_operation_categories_operation_types_type_id",
                table: "operation_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_operations_departments_department_id",
                table: "operations");

            migrationBuilder.DropIndex(
                name: "ix_operation_categories_type_id",
                table: "operation_categories");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "operation_categories");

            migrationBuilder.CreateIndex(
                name: "ix_operations_type_id",
                table: "operations",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_operations_department_department_id",
                table: "operations",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_operations_operation_types_type_id",
                table: "operations",
                column: "type_id",
                principalTable: "operation_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
