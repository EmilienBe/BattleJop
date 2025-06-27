using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using System.Net;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Tournaments;

public class GetTournamentsTest(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory>(factory)
{
    [Fact]
    public async Task GetTournaments_ShouldReturn200_WhenNoTournamentsExists()
    {
        //Act
        var response = await _client.GetAsync($"tournaments");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ICollection<TournamentResponse>>(json);

        Assert.NotNull(data);
        Assert.False(data.Any());
    }

    [Fact]
    public async Task GetTournaments_ShouldReturn200_WhenTournamentIsCreated()
    {
        //Arrange
        var tournamentName1 = "Tournament test 1";
        var tournamentId1 = Guid.NewGuid();

        var tournament1 = new Tournament(tournamentId1, tournamentName1);

        var tournamentName2 = "Tournament test 2";
        var tournamentId2 = Guid.NewGuid();

        var tournament2 = new Tournament(tournamentId2, tournamentName2);

        _context.Tournaments.Add(tournament1);
        _context.Tournaments.Add(tournament2);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<TournamentResponse>>(json);

        Assert.NotNull(data);
        Assert.Equal(2, data.Count);

        var actualTournament1 = data.First(data => data.Id == tournamentId1);
        Assert.NotNull(actualTournament1);
        Assert.Equal(tournamentId1, actualTournament1.Id);
        Assert.Equal(tournamentName1, actualTournament1.Name);
        Assert.Equal(TournamentState.InConfiguration, actualTournament1.State);

        var actualTournament2 = data.First(data => data.Id == tournamentId2);
        Assert.NotNull(actualTournament2);
        Assert.Equal(tournamentId2, actualTournament2.Id);
        Assert.Equal(tournamentName2, actualTournament2.Name);
        Assert.Equal(TournamentState.InConfiguration, actualTournament2.State);

        ClearDatabase();
    }
}
