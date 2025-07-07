using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web;
using System.Net;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Rounds;

public class GetRoundByIdTest(BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory>(factory)
{
    [Fact]
    public async Task GetRoundById_ShouldReturn404_WhenTournamentNoExist()
    {
        //Arrange
        var tournamentId = Guid.NewGuid();
        var roundId = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournamentId}/rounds/{roundId}");

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
    public async Task GetRoundById_ShouldReturn404_WhenRoundNoExist()
    {
        //Arrange
        var roundId = Guid.NewGuid();
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");

        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/rounds/{roundId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10009, data.Code);
        Assert.Equal("ROUND_NOT_FOUND", data.Error);
        Assert.Equal($"The round with identifier '{roundId}' does not exist.", data.Message);

        ClearDatabase();
    }

    [Fact]
    public async Task GetRoundById_ShouldReturn409_WithRoundHasNotStartedState()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");
        var round = new Round(Guid.NewGuid(), 1, tournament);

        _context.Tournaments.Add(tournament);
        _context.Rounds.Add(round);

        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/rounds/{round.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(10010, data.Code);
        Assert.Equal("ROUND_INVALID_STATE", data.Error);
        Assert.Equal($"The round must have started or been completed to retrieve the details.", data.Message);

        ClearDatabase();
    }

    [Fact]
    public async Task GetRoundById_ShouldReturn200_WithRound()
    {
        //Arrange
        var tournament = new Tournament(Guid.NewGuid(), "Test Tournament");
        var round = new Round(Guid.NewGuid(), 1, tournament);
        var match = new Match(Guid.NewGuid(), 1, round);

        round.UpdateState(RoundState.InProgress);

        var team1 = new Team(Guid.NewGuid(), "Team 1", tournament);
        var team2 = new Team(Guid.NewGuid(), "Team 2", tournament);
        var player1 = new Player(Guid.NewGuid(), "Player 1", team1);
        var player2 = new Player(Guid.NewGuid(), "Player 2", team2);

        var score1 = new MatchTeam(Guid.NewGuid(), match, team1);
        var score2 = new MatchTeam(Guid.NewGuid(), match, team2);

        _context.Tournaments.Add(tournament);
        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.Players.Add(player1);
        _context.Players.Add(player2);
        _context.Rounds.Add(round);
        _context.Matchs.Add(match);
        _context.MatchTeams.Add(score1);
        _context.MatchTeams.Add(score2);

        _context.SaveChanges();

        //Act
        var response = await _client.GetAsync($"tournaments/{tournament.Id}/rounds/{round.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<RoundDetailResponse>(json);

        Assert.NotNull(data);
        Assert.Equal(round.Id, data.Id);
        Assert.Equal(round.RunningOrder, data.RunningOrder);
        Assert.Equal(round.State, data.State);
        Assert.Single(data.Matchs);
        Assert.Equal(match.Id, data.Matchs.First().Id);
        Assert.Equal(match.RunningOrder, data.Matchs.First().RunningOrder);
        Assert.Equal(match.State, data.Matchs.First().State);

        Assert.False(data.Matchs.First().FirstTeamScore.IsWinner);
        Assert.Equal(team1.Name, data.Matchs.First().FirstTeamScore.TeamName);
        Assert.Equal(0,data.Matchs.First().FirstTeamScore.RemainingPuck);
        Assert.Equal(team1.Id, data.Matchs.First().FirstTeamScore.TeamId);
        Assert.Equal(0,data.Matchs.First().FirstTeamScore.Score);

        Assert.False(data.Matchs.First().SecondTeamScore.IsWinner);
        Assert.Equal(team2.Name, data.Matchs.First().SecondTeamScore.TeamName);
        Assert.Equal(0, data.Matchs.First().SecondTeamScore.RemainingPuck);
        Assert.Equal(team2.Id, data.Matchs.First().SecondTeamScore.TeamId);
        Assert.Equal(0, data.Matchs.First().SecondTeamScore.Score);

        ClearDatabase();
    }
}
