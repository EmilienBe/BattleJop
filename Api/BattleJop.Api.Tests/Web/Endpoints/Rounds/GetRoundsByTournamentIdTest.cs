using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web;
using System.Net;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Rounds;

public class GetRoundsByTournamentIdTest(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory>(factory)
{
    [Fact]
    public async Task GetRounds_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var tournamentId = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournamentId}/rounds");

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
    public async Task GetRounds_ShouldReturn200_WithoutRounds()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/rounds");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<RoundResponse>>(json);

        Assert.NotNull(data);
        Assert.False(data.Any());

        ClearDatabase();
    }

    [Fact]
    public async Task GetRounds_ShouldReturn200_WithRounds()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        var round = new Round(Guid.NewGuid(), 1, tournament);

        _context.Tournaments.Add(tournament);
        _context.Rounds.Add(round);

        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/rounds");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<RoundResponse>>(json);

        Assert.NotNull(data);
        Assert.Single(data);
        Assert.Equal(round.Id, data.First().Id);
        Assert.Equal(round.State, data.First().State);
        Assert.Equal(round.RunningOrder, data.First().RunningOrder);

        ClearDatabase();
    }
}
