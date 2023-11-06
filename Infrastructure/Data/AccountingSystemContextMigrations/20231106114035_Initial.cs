#nullable disable

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.AccountingSystemContextMigrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("operation_categories",
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
                                 table.PrimaryKey("pk_operation_categories", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("operation_types",
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
                                 table.PrimaryKey("pk_operation_types", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("positions",
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
                                 table.PrimaryKey("pk_positions", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("operations",
                             table => new
                             {
                                 id = table.Column<long>("bigint", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 type_id = table.Column<long>("bigint", nullable: false),
                                 category_id = table.Column<long>("bigint", nullable: false),
                                 comment = table.Column<string>("longtext", nullable: true)
                                                .Annotation("MySql:CharSet", "utf8mb4"),
                                 sum = table.Column<decimal>("decimal(65,30)", nullable: false),
                                 date = table.Column<DateTime>("datetime(6)", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_operations", x => x.id);

                                 table.ForeignKey
                                     ("fk_operations_operation_categories_category_id",
                                      x => x.category_id,
                                      "operation_categories",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);

                                 table.ForeignKey
                                     ("fk_operations_operation_types_type_id",
                                      x => x.type_id,
                                      "operation_types",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("employee",
                             table => new
                             {
                                 id = table.Column<long>("bigint", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 name = table.Column<string>("longtext", nullable: false)
                                             .Annotation("MySql:CharSet", "utf8mb4"),
                                 surname = table.Column<string>("longtext", nullable: false)
                                                .Annotation("MySql:CharSet", "utf8mb4"),
                                 date_of_birth = table.Column<DateTime>("datetime(6)", nullable: false),
                                 phone_number = table.Column<string>("longtext", nullable: false)
                                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                 salary = table.Column<decimal>("decimal(65,30)", nullable: false),
                                 position_id = table.Column<long>("bigint", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_employee", x => x.id);

                                 table.ForeignKey
                                     ("fk_employee_positions_position_id",
                                      x => x.position_id,
                                      "positions",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex
            ("ix_employee_position_id",
             "employee",
             "position_id");

        migrationBuilder.CreateIndex
            ("ix_operations_category_id",
             "operations",
             "category_id");

        migrationBuilder.CreateIndex
            ("ix_operations_type_id",
             "operations",
             "type_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("employee");

        migrationBuilder.DropTable("operations");

        migrationBuilder.DropTable("positions");

        migrationBuilder.DropTable("operation_categories");

        migrationBuilder.DropTable("operation_types");
    }
}