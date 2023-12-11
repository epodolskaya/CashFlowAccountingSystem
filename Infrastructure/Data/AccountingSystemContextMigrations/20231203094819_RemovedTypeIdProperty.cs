#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.AccountingSystemContextMigrations;

/// <inheritdoc />
public partial class RemovedTypeIdProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn
            ("type_id",
             "operations");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<long>
            ("type_id",
             "operations",
             "bigint",
             nullable: false,
             defaultValue: 0L);
    }
}