using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using System.Net;
using System.Text.Json;
using BattleJop.Api.Web;

namespace BattleJop.Api.Tests.Web.Endpoints.Tournaments;

public class GetTournamentByIdTest(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory> (factory)
{
    [Fact]
    public async Task GetTournamentById_ShouldReturn201_WhenTournamentIsCreated()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();


        //Act
        var response = await _client.GetAsync($"tournaments/{id}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<TournamentResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(id, data.Id);
        Assert.Equal(name, data.Name);
        Assert.Equal(TournamentState.InConfiguration, data.State);

        ClearDatabase();
    }

    [Fact] 
    public async Task GetTournamentById_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"tournaments/{id}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10001, data.Code);
        Assert.Equal("TOURNAMENT_NOT_FOUND", data.Error);
        Assert.Equal($"The tournament with identifier '{id}' does not exist.", data.Message);
    }
}
