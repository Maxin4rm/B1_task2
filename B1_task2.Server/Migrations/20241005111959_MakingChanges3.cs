using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_task2.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakingChanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentAccountID",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentAccountID",
                table: "Accounts",
                type: "integer",
                nullable: true);
        }
    }
}
