using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget.Api.Migrations
{
    public partial class AccountlessForecast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forecasts_BankAccounts_AccountId",
                table: "Forecasts");

            migrationBuilder.DropIndex(
                name: "IX_Forecasts_AccountId",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Forecasts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Forecasts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_AccountId",
                table: "Forecasts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_BankAccounts_AccountId",
                table: "Forecasts",
                column: "AccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
