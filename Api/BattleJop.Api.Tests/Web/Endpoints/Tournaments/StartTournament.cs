using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web;
using System.Net;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Tournaments;

public class StartTournament(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory>(factory)
{
    [Fact]
    public async Task StartTournament_ShouldReturn404_WhenTournamentNoExst()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);
        
        Assert.NotNull(data);
        Assert.Equal(10001, data.Code);
        Assert.Equal("TOURNAMENT_NOT_FOUND", data.Error);
        Assert.Equal($"The tournament with identifier '{id}' does not exist.", data.Message);

    }

    [Fact]
    public async Task StartTournament_ShouldReturn409_WhenTournamentStateIsInvalid()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        tournament.UpdateState(TournamentState.InProgress);

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10006, data.Code);
        Assert.Equal("TOURNAMENT_INVALID_STATE", data.Error);
        Assert.Equal($"The tournament is in state 'InProgress', which is invalid for starting.", data.Message);

    }

    [Fact]
    public async Task StartTournament_ShouldReturn409_WhenTournamentInvalidNumberTeamNonEven()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        var team1 = new Team(Guid.NewGuid(), "Team 1", tournament);
        var team2 = new Team(Guid.NewGuid(), "Team 2", tournament);
        var team3 = new Team(Guid.NewGuid(), "Team 3", tournament);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.Teams.Add(team3);

        _context.SaveChanges();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10007, data.Code);
        Assert.Equal("TOURNAMENT_INVALID_NUMBER_TEAMS", data.Error);
        Assert.Equal($"The tournament must start with an even number of teams.", data.Message);

    }

    [Fact]
    public async Task StartTournament_ShouldReturn409_WhenTournamentInvalidNumberNotEnough()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        var team1 = new Team(Guid.NewGuid(), "Team 1", tournament);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(team1);

        _context.SaveChanges();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10007, data.Code);
        Assert.Equal("TOURNAMENT_INVALID_NUMBER_TEAMS", data.Error);
        Assert.Equal($"The tournament must start with a minimum of two teams.", data.Message);

    }

    [Fact]
    public async Task StartTournament_ShouldReturn409_WhenTournamentHasNoRound()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        var team1 = new Team(Guid.NewGuid(), "Team 1", tournament);
        var team2 = new Team(Guid.NewGuid(), "Team 2", tournament);
        var team3 = new Team(Guid.NewGuid(), "Team 3", tournament);
        var team4 = new Team(Guid.NewGuid(), "Team 4", tournament);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.Teams.Add(team3);
        _context.Teams.Add(team4);

        _context.SaveChanges();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10008, data.Code);
        Assert.Equal("TOURNAMENT_NO_ROUND_EXIST", data.Error);
        Assert.Equal($"The tournament has no rounds.", data.Message);

    }

    [Fact]
    public async Task StartTournament_ShouldReturn204_WhenTournamentStarted()
    {
        //Arrange
        var name = "Tournament test";
        var id = Guid.NewGuid();

        var tournament = new Tournament(id, name);

        var team1 = new Team(Guid.NewGuid(), "Team 1", tournament);
        var team2 = new Team(Guid.NewGuid(), "Team 2", tournament);
        var team3 = new Team(Guid.NewGuid(), "Team 3", tournament);
        var team4 = new Team(Guid.NewGuid(), "Team 4", tournament);


        var round = new Round(Guid.NewGuid(), 1, tournament);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.Teams.Add(team3);
        _context.Teams.Add(team4);
        _context.Rounds.Add(round);

        _context.SaveChanges();

        //Act
        var response = await _client.PostAsync($"tournaments/{id}/start", null);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
