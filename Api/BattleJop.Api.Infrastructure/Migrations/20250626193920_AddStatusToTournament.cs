using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleJop.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Tournaments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Tournaments");
        }
    }
}
