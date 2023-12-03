using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.AccountingSystemContextMigrations
{
    /// <inheritdoc />
    public partial class RemovedTypeIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type_id",
                table: "operations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "type_id",
                table: "operations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
