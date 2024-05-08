using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodOffer.Infrastructure.Migrations
{
    public partial class test01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Client",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Advertising",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertising", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertising");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Client");
        }
    }
}
