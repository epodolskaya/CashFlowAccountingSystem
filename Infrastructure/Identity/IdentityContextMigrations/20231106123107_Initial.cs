#nullable disable

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Identity.IdentityContextMigrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetRoles",
                             table => new
                             {
                                 id = table.Column<long>("bigint", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                             .Annotation("MySql:CharSet", "utf8mb4"),
                                 normalized_name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                                        .Annotation("MySql:CharSet", "utf8mb4"),
                                 concurrency_stamp = table.Column<string>("longtext", nullable: true)
                                                          .Annotation("MySql:CharSet", "utf8mb4")
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_asp_net_roles", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetUsers",
                             table => new
                             {
                                 id = table.Column<long>("bigint", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 employee_id = table.Column<long>("bigint", nullable: false),
                                 user_name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                                  .Annotation("MySql:CharSet", "utf8mb4"),
                                 normalized_user_name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                                             .Annotation("MySql:CharSet", "utf8mb4"),
                                 email = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                 normalized_email = table.Column<string>("varchar(256)", maxLength: 256, nullable: true)
                                                         .Annotation("MySql:CharSet", "utf8mb4"),
                                 email_confirmed = table.Column<bool>("tinyint(1)", nullable: false),
                                 password_hash = table.Column<string>("longtext", nullable: true)
                                                      .Annotation("MySql:CharSet", "utf8mb4"),
                                 security_stamp = table.Column<string>("longtext", nullable: true)
                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                 concurrency_stamp = table.Column<string>("longtext", nullable: true)
                                                          .Annotation("MySql:CharSet", "utf8mb4"),
                                 phone_number = table.Column<string>("longtext", nullable: true)
                                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                 phone_number_confirmed = table.Column<bool>("tinyint(1)", nullable: false),
                                 two_factor_enabled = table.Column<bool>("tinyint(1)", nullable: false),
                                 lockout_end = table.Column<DateTimeOffset>("datetime(6)", nullable: true),
                                 lockout_enabled = table.Column<bool>("tinyint(1)", nullable: false),
                                 access_failed_count = table.Column<int>("int", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_asp_net_users", x => x.id);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetRoleClaims",
                             table => new
                             {
                                 id = table.Column<int>("int", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 role_id = table.Column<long>("bigint", nullable: false),
                                 claim_type = table.Column<string>("longtext", nullable: true)
                                                   .Annotation("MySql:CharSet", "utf8mb4"),
                                 claim_value = table.Column<string>("longtext", nullable: true)
                                                    .Annotation("MySql:CharSet", "utf8mb4")
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_asp_net_role_claims", x => x.id);

                                 table.ForeignKey
                                     ("fk_asp_net_role_claims_asp_net_roles_role_id",
                                      x => x.role_id,
                                      "AspNetRoles",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetUserClaims",
                             table => new
                             {
                                 id = table.Column<int>("int", nullable: false)
                                           .Annotation
                                               ("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                 user_id = table.Column<long>("bigint", nullable: false),
                                 claim_type = table.Column<string>("longtext", nullable: true)
                                                   .Annotation("MySql:CharSet", "utf8mb4"),
                                 claim_value = table.Column<string>("longtext", nullable: true)
                                                    .Annotation("MySql:CharSet", "utf8mb4")
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey("pk_asp_net_user_claims", x => x.id);

                                 table.ForeignKey
                                     ("fk_asp_net_user_claims_asp_net_users_user_id",
                                      x => x.user_id,
                                      "AspNetUsers",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetUserLogins",
                             table => new
                             {
                                 login_provider = table.Column<string>("varchar(255)", nullable: false)
                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                 provider_key = table.Column<string>("varchar(255)", nullable: false)
                                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                 provider_display_name = table.Column<string>("longtext", nullable: true)
                                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                 user_id = table.Column<long>("bigint", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey
                                     ("pk_asp_net_user_logins",
                                      x => new
                                      {
                                          x.login_provider,
                                          x.provider_key
                                      });

                                 table.ForeignKey
                                     ("fk_asp_net_user_logins_asp_net_users_user_id",
                                      x => x.user_id,
                                      "AspNetUsers",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetUserRoles",
                             table => new
                             {
                                 user_id = table.Column<long>("bigint", nullable: false),
                                 role_id = table.Column<long>("bigint", nullable: false)
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey
                                     ("pk_asp_net_user_roles",
                                      x => new
                                      {
                                          x.user_id,
                                          x.role_id
                                      });

                                 table.ForeignKey
                                     ("fk_asp_net_user_roles_asp_net_roles_role_id",
                                      x => x.role_id,
                                      "AspNetRoles",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);

                                 table.ForeignKey
                                     ("fk_asp_net_user_roles_asp_net_users_user_id",
                                      x => x.user_id,
                                      "AspNetUsers",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable
                            ("AspNetUserTokens",
                             table => new
                             {
                                 user_id = table.Column<long>("bigint", nullable: false),
                                 login_provider = table.Column<string>("varchar(255)", nullable: false)
                                                       .Annotation("MySql:CharSet", "utf8mb4"),
                                 name = table.Column<string>("varchar(255)", nullable: false)
                                             .Annotation("MySql:CharSet", "utf8mb4"),
                                 value = table.Column<string>("longtext", nullable: true)
                                              .Annotation("MySql:CharSet", "utf8mb4")
                             },
                             constraints: table =>
                             {
                                 table.PrimaryKey
                                     ("pk_asp_net_user_tokens",
                                      x => new
                                      {
                                          x.user_id,
                                          x.login_provider,
                                          x.name
                                      });

                                 table.ForeignKey
                                     ("fk_asp_net_user_tokens_asp_net_users_user_id",
                                      x => x.user_id,
                                      "AspNetUsers",
                                      "id",
                                      onDelete: ReferentialAction.Cascade);
                             })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex
            ("ix_asp_net_role_claims_role_id",
             "AspNetRoleClaims",
             "role_id");

        migrationBuilder.CreateIndex
            ("RoleNameIndex",
             "AspNetRoles",
             "normalized_name",
             unique: true);

        migrationBuilder.CreateIndex
            ("ix_asp_net_user_claims_user_id",
             "AspNetUserClaims",
             "user_id");

        migrationBuilder.CreateIndex
            ("ix_asp_net_user_logins_user_id",
             "AspNetUserLogins",
             "user_id");

        migrationBuilder.CreateIndex
            ("ix_asp_net_user_roles_role_id",
             "AspNetUserRoles",
             "role_id");

        migrationBuilder.CreateIndex
            ("EmailIndex",
             "AspNetUsers",
             "normalized_email");

        migrationBuilder.CreateIndex
            ("UserNameIndex",
             "AspNetUsers",
             "normalized_user_name",
             unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("AspNetRoleClaims");

        migrationBuilder.DropTable("AspNetUserClaims");

        migrationBuilder.DropTable("AspNetUserLogins");

        migrationBuilder.DropTable("AspNetUserRoles");

        migrationBuilder.DropTable("AspNetUserTokens");

        migrationBuilder.DropTable("AspNetRoles");

        migrationBuilder.DropTable("AspNetUsers");
    }
}