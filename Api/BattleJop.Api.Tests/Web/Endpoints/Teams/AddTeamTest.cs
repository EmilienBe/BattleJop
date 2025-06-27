using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using BattleJop.Api.Web;

namespace BattleJop.Api.Tests.Web.Endpoints.Teams;

public class AddTeamTest(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory>(factory)
{
    [Fact]
    public async Task AddTeam_ShouldReturn201_WhenTeamIsCreated()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        var name = "Team 789";
        var players = new List<string> { "Player 1", "Player 2" };

        //Act
        var response = await _client.PostAsJsonAsync($"tournaments/{tournament.Id}/teams", new AddTeamRequest
        {
            Name = name,
            Players = players
        });

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<TeamResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(name, data.Name);
        Assert.Equal(2, data.Players.Count);
        Assert.True(data.Players.Select(p => p.Name).Contains("Player 1"));
        Assert.True(data.Players.Select(p => p.Name).Contains("Player 2"));

        ClearDatabase();
    }

    [Fact]
    public async Task AddTeam_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var name = "Team 1";
        var players = new string[] { "Player 1", "Player 2" };
        var tournamentId = Guid.NewGuid();

        //Act
        var response = await _client.PostAsJsonAsync($"tournaments/{tournamentId}/teams", new AddTeamRequest
        {
            Name = name,
            Players = [.. players]
        });

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10001, data.Code);
        Assert.Equal("TOURNAMENT_NOT_FOUND", data.Error);
        Assert.Equal($"The tournament with identifier '{tournamentId}' does not exist.", data.Message);
    }

    [Theory]
    [InlineData("", new string[] { "Player 1", "Player 2"})]
    [InlineData("Team 1", null)]
    [InlineData("Team 1", new string[] { " ", "Player 2" })]
    public async Task AddTeam_ShouldReturn400_WhenInputIsInvalid(string name, string[] players)
    {
        //Act
        var response = await _client.PostAsJsonAsync("tournaments/2257a071-6240-4966-ad7d-2fd9b7001186/teams", new AddTeamRequest
        {
            Name = name,
            Players = players?.ToList()
        });

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
