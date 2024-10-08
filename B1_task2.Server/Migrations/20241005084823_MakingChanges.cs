using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_task2.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BalanceID",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileInfo",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BalanceID",
                table: "Files",
                column: "BalanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountID",
                table: "Accounts",
                column: "ParentAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ParentAccountID",
                table: "Accounts",
                column: "ParentAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Balances_BalanceID",
                table: "Files",
                column: "BalanceID",
                principalTable: "Balances",
                principalColumn: "BalanceID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentAccountID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Balances_BalanceID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_BalanceID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentAccountID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BalanceID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileInfo",
                table: "Files");
        }
    }
}
