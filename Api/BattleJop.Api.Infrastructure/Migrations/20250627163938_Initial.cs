using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleJop.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RunningOrder = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rounds_tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teams_tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RunningOrder = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matchs_rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_players_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "match_teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    IsWinner = table.Column<bool>(type: "boolean", nullable: false),
                    RemainingPuck = table.Column<int>(type: "integer", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desactivated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match_teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_match_teams_matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_match_teams_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_match_teams_MatchId",
                table: "match_teams",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_match_teams_TeamId",
                table: "match_teams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matchs_RoundId",
                table: "matchs",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_players_TeamId",
                table: "players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_TournamentId",
                table: "rounds",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_TournamentId",
                table: "teams",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "match_teams");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "matchs");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "tournaments");
        }
    }
}
