using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAPI.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "centralbankofturkey",
                columns: table => new
                {
                    Currencyid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Currencyname = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Currencyid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "dailyrecords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Currencyid = table.Column<int>(type: "int", nullable: true),
                    Forexselling = table.Column<decimal>(type: "decimal(13,4)", precision: 13, scale: 4, nullable: true),
                    Exchangedate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyrecords", x => x.id);
                    table.ForeignKey(
                        name: "usdtry_ibfk_1",
                        column: x => x.Currencyid,
                        principalTable: "centralbankofturkey",
                        principalColumn: "Currencyid");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.InsertData(
                table: "centralbankofturkey",
                columns: new[] { "Currencyid", "Currencyname" },
                values: new object[] { 1, "USD" });

            migrationBuilder.InsertData(
                table: "centralbankofturkey",
                columns: new[] { "Currencyid", "Currencyname" },
                values: new object[] { 2, "EUR" });

            migrationBuilder.InsertData(
                table: "centralbankofturkey",
                columns: new[] { "Currencyid", "Currencyname" },
                values: new object[] { 3, "GBP" });

            migrationBuilder.CreateIndex(
                name: "Currencyid",
                table: "dailyrecords",
                column: "Currencyid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyrecords");

            migrationBuilder.DropTable(
                name: "centralbankofturkey");
        }
    }
}
