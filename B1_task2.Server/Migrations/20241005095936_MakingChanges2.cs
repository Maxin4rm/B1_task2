using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_task2.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakingChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentAccountID",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentAccountID",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
