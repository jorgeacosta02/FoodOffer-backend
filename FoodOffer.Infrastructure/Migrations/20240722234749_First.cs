using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodOffer.Infrastructure.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    add_ref_id = table.Column<int>(type: "int", nullable: false),
                    add_ref_type = table.Column<string>(type: "varchar(1)", nullable: false),
                    add_item = table.Column<short>(type: "smallint", nullable: false),
                    add_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    add_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    add_cit_cod = table.Column<short>(type: "smallint", nullable: false),
                    add_ste_cod = table.Column<short>(type: "smallint", nullable: false),
                    add_cou_cod = table.Column<short>(type: "smallint", nullable: false),
                    add_obs = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => new { x.add_ref_id, x.add_ref_type, x.add_item });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertising_attributes",
                columns: table => new
                {
                    ada_adv_id = table.Column<int>(type: "int", nullable: false),
                    ada_atr_cod = table.Column<short>(type: "smallint", nullable: false),
                    ada_value = table.Column<string>(type: "varchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertising_attributes", x => new { x.ada_adv_id, x.ada_atr_cod });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertising_categories",
                columns: table => new
                {
                    cat_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cat_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertising_categories", x => x.cat_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertising_images",
                columns: table => new
                {
                    adi_adv_id = table.Column<int>(type: "int", nullable: false),
                    adi_item = table.Column<short>(type: "smallint", nullable: false),
                    adi_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    adi_path = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertising_images", x => new { x.adi_adv_id, x.adi_item });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertising_states",
                columns: table => new
                {
                    ads_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ads_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertising_states", x => x.ads_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertising_time_settings",
                columns: table => new
                {
                    ats_adv_id = table.Column<int>(type: "int", nullable: false),
                    ats_day = table.Column<short>(type: "smallint", nullable: false),
                    ats_start_1 = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ats_end_1 = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ats_nextday_1 = table.Column<string>(type: "varchar(1)", nullable: true),
                    ats_start_2 = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ats_end_2 = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ats_nextday_2 = table.Column<string>(type: "varchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertising_time_settings", x => new { x.ats_adv_id, x.ats_day });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertisings",
                columns: table => new
                {
                    adv_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    adv_com_id = table.Column<int>(type: "int", nullable: false),
                    adv_title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    adv_desc = table.Column<string>(type: "longtext", nullable: false),
                    adv_price = table.Column<double>(type: "double", nullable: false),
                    adv_ads_cod = table.Column<short>(type: "smallint", nullable: false),
                    adv_cat_cod = table.Column<short>(type: "smallint", nullable: false),
                    adv_prl_cod = table.Column<short>(type: "smallint", nullable: false),
                    adv_create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    adv_delete_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    adv_update_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisings", x => x.adv_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "advertisings_address",
                columns: table => new
                {
                    aad_adv_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    aad_adv_com_id = table.Column<int>(type: "int", nullable: false),
                    add_add_item = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisings_address", x => x.aad_adv_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attribute_categories",
                columns: table => new
                {
                    atc_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    atc_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_categories", x => x.atc_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    atr_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    atr_atc_cod = table.Column<short>(type: "smallint", nullable: false),
                    atr_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.atr_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    cit_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cit_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    cit_ste_cod = table.Column<short>(type: "smallint", nullable: false),
                    cit_cou_cod = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.cit_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commerce_attributes",
                columns: table => new
                {
                    coa_com_id = table.Column<int>(type: "int", nullable: false),
                    coa_atr_id = table.Column<short>(type: "smallint", nullable: false),
                    coa_value = table.Column<string>(type: "varchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commerce_attributes", x => new { x.coa_com_id, x.coa_atr_id });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commerce_images",
                columns: table => new
                {
                    coi_com_id = table.Column<int>(type: "int", nullable: false),
                    coi_item = table.Column<short>(type: "smallint", nullable: false),
                    coi_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    coi_path = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commerce_images", x => new { x.coi_com_id, x.coi_item });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commerce_types",
                columns: table => new
                {
                    cot_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cot_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commerce_types", x => x.cot_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commerces",
                columns: table => new
                {
                    com_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    com_usr_id = table.Column<int>(type: "int", nullable: false),
                    com_cot_cod = table.Column<short>(type: "smallint", nullable: false),
                    com_mail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    com_phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    com_cell_phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    com_web_url = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commerces", x => x.com_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    cou_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cou_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.cou_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identification_types",
                columns: table => new
                {
                    ide_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ide_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identification_types", x => x.ide_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "priority_levels",
                columns: table => new
                {
                    prl_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    prl_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority_levels", x => x.prl_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    ste_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ste_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ste_cou_cod = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.ste_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_login_data",
                columns: table => new
                {
                    uld_usr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    uld_email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    uld_pwd = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    uld_salt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_login_data", x => x.uld_usr_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_types",
                columns: table => new
                {
                    ust_cod = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ust_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types", x => x.ust_cod);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    usr_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    usr_ide_cod = table.Column<short>(type: "smallint", nullable: true),
                    usr_ide_num = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    usr_mail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    usr_phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    usr_cell_phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    usr_ust_cod = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.usr_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "advertising_attributes");

            migrationBuilder.DropTable(
                name: "advertising_categories");

            migrationBuilder.DropTable(
                name: "advertising_images");

            migrationBuilder.DropTable(
                name: "advertising_states");

            migrationBuilder.DropTable(
                name: "advertising_time_settings");

            migrationBuilder.DropTable(
                name: "advertisings");

            migrationBuilder.DropTable(
                name: "advertisings_address");

            migrationBuilder.DropTable(
                name: "attribute_categories");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "commerce_attributes");

            migrationBuilder.DropTable(
                name: "commerce_images");

            migrationBuilder.DropTable(
                name: "commerce_types");

            migrationBuilder.DropTable(
                name: "commerces");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "identification_types");

            migrationBuilder.DropTable(
                name: "priority_levels");

            migrationBuilder.DropTable(
                name: "states");

            migrationBuilder.DropTable(
                name: "user_login_data");

            migrationBuilder.DropTable(
                name: "user_types");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
