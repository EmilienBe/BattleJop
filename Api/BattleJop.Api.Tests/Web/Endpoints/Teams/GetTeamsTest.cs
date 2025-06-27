using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Tests.Web.Endpoints.Teams;

public class GetTeamsTest (BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory> (factory)
{
    [Fact]
    public async Task GetTeams_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var tournamentId = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournamentId}/teams");

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
    public async Task GetTeams_ShouldReturn200_WithoutTeams()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/teams");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<TeamResponse>>(json);

        Assert.NotNull(data);
        Assert.False(data.Any());
    }

    [Fact]
    public async Task GetTeams_ShouldReturn200_WithTeams()
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
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/teams");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<TeamResponse>>(json);

        Assert.NotNull(data);
        Assert.Single(data);
        Assert.Equal(teams.Id, data.First().Id);
        Assert.Equal(teams.Name, data.First().Name);
        Assert.Equal(teams.Players.First().Id, data.First().Players.First().Id);
        Assert.Equal(teams.Players.First().Name, data.First().Players.First().Name);
    }
}
