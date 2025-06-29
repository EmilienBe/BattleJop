using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web;
using BattleJop.Api.Web.Dtos;
using System.Net;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Teams;

public class GetTeamByIdTest (BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory> (factory)
{
    [Fact]
    public async Task GetTeamById_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var tournamentId = Guid.NewGuid();
        var teamId = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournamentId}/teams/{teamId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10001, data.Code);
        Assert.Equal("TOURNAMENT_NOT_FOUND", data.Error);
        Assert.Equal($"The tournament with identifier '{tournamentId}' does not exist.", data.Message);
    }

    [Fact]
    public async Task GetTeamsById_ShouldReturn404_WhenTeamNoExist()
    {
        //Arrange
        var teamId = Guid.NewGuid();
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/teams/{teamId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10002, data.Code);
        Assert.Equal("TEAM_NOT_FOUND", data.Error);
        Assert.Equal($"The team with identifier '{teamId}' does not exist.", data.Message);

        ClearDatabase();
    }

    [Fact]
    public async Task GetTeamsById_ShouldReturn200_WithTeams()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");
        var teams = new Team(Guid.NewGuid(), "Team 1", tournament);
        var player = new Player(Guid.NewGuid(), "Player 1", teams);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(teams);
        _context.Players.Add(player);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/teams/{teams.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<TeamResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(teams.Id, data.Id);
        Assert.Equal(teams.Name, data.Name);
        Assert.Equal(teams.Players.First().Id, data.Players.First().Id);
        Assert.Equal(teams.Players.First().Name, data.Players.First().Name);

        ClearDatabase();
    }
}
