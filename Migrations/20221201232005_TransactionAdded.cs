using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Migrations
{
    public partial class TransactionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromUserCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToUserCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoneyAmount = table.Column<float>(type: "real", nullable: false),
                    FromUserInitialBalance = table.Column<float>(type: "real", nullable: false),
                    FromUserAfterBalance = table.Column<float>(type: "real", nullable: false),
                    ToUserInitialBalance = table.Column<float>(type: "real", nullable: false),
                    ToUserAfterBalance = table.Column<float>(type: "real", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
