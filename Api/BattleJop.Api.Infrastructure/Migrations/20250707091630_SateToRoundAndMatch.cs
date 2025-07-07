using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleJop.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SateToRoundAndMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "rounds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "matchs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "rounds");

            migrationBuilder.DropColumn(
                name: "State",
                table: "matchs");
        }
    }
}
