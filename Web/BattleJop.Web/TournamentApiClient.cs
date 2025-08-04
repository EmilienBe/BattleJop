using BattleJop.Web.Dao;
using BattleJop.Web.Dto;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
namespace BattleJop.Web;

public class TournamentApiClient(HttpClient httpClient, TournamentState tournamentState, ProtectedLocalStorage localStorage)
{
    private JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    public async Task<RoundDto> GetCurrentRound()
    {
        var currentRoundId = tournamentState.CurrentTournamentId ?? (await localStorage.GetAsync<Guid>("tournamentId")).Value;
        var rounds = await httpClient.GetFromJsonAsync<IEnumerable<RoundDao>>($"/tournaments/{currentRoundId}/rounds/").ConfigureAwait(false);
        var currentRound = rounds?.OrderBy(r => r.RunningOrder).FirstOrDefault(r => r.State == Enum.RoundState.InProgress);

        var roundDao = await httpClient.GetFromJsonAsync<RoundDao>($"/tournaments/{currentRoundId}/rounds/{currentRound?.Id}").ConfigureAwait(false);

        RoundDto round = new RoundDto()
        {
            Id = roundDao.Id,
            RunningOrder = roundDao.RunningOrder,
            State = roundDao.State,
            Matchs = roundDao.Matchs.Select(m => new MatchDto
            {
                Id = m.Id,
                TeamA = new TeamDto
                {
                    Id = m.FirstTeam.TeamId,
                    Name = m.FirstTeam.TeamName,
                },
                TeamB = new TeamDto
                {
                    Id = m.SecondTeam.TeamId,
                    Name = m.SecondTeam.TeamName,
                },
                ScoreA = m.FirstTeam.Score,
                ScoreB = m.SecondTeam.Score,
                RemainingPuckA = m.FirstTeam.RemainingPuck,
                RemainingPuckB = m.SecondTeam.RemainingPuck,
                State = m.FirstTeam.Score != 0 || m.SecondTeam.Score != 0
                    ? Enum.MatchState.Ended
                    : Enum.MatchState.InProgress
            }).ToList()
        };

        return round;
    }

    public async Task<List<RankingDto>> GetRankings()
    {
        var currentRoundId = tournamentState.CurrentTournamentId ?? (await localStorage.GetAsync<Guid>("tournamentId")).Value;
        var ranks = await httpClient.GetFromJsonAsync<IEnumerable<RankDao>>($"/tournaments/{currentRoundId}/ranking");

        return ranks.Select(r => new RankingDto
        {
            Team = new TeamDto
            {
                Id = r.TeamId,
                Name = r.TeamName,
            },
            Wins = r.NumberOfVictory,
            Losses = r.NumberOfDefeat,
            Pucks = r.TotalRemainingPuck,
            Points = r.TotalScore
        })
            .OrderByDescending(r => r.Wins)
            .ThenByDescending(r => r.Points)
            .ThenByDescending(r => r.Pucks).
            ToList();
    }

    public async Task<Guid> CreateTournament(string name, int rounds, List<TeamDto> teams)
    {
        var dictionary = new Dictionary<string, object>
        {
            { "name", name },
            { "numberRounds", rounds }
        };
        var response = await httpClient.PostAsync("/tournaments", new StringContent(JsonSerializer.Serialize(dictionary, jsonOptions), System.Text.Encoding.UTF8, "application/json"));
        var jsonString = await response.Content.ReadAsStringAsync();
        Guid tournamentId = JsonDocument.Parse(jsonString).RootElement.GetProperty("id").GetGuid();

        AddTeams(tournamentId, teams);

        await httpClient.PostAsync($"/tournaments/${tournamentId}/start", null);

        return tournamentId;
    }

    private void AddTeams(Guid tournamentId, List<TeamDto> teams)
    {
        var tasks = new List<Task>();
        foreach (var team in teams)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "name", team.Name },
                { "players", new[] { team.Players.Item1, team.Players.Item2 } }
            };
            var content = new StringContent(JsonSerializer.Serialize(dictionary, jsonOptions), System.Text.Encoding.UTF8, "application/json");
            tasks.Add(httpClient.PostAsync($"/tournaments/{tournamentId}/teams", content));
        }

        Task.WaitAll([.. tasks]);
    }

    public async Task<Guid> GetCurrentTournamentId()
    {
        var tournaments = await httpClient.GetFromJsonAsync<IEnumerable<TournamentDao>>("/tournaments");
        return tournaments.FirstOrDefault(t => t.State == "InProgress").Id;
    }

    public async Task EndMatch(MatchDto match)
    {
        var dictionnary = new Dictionary<string, object>()
        {
            {"firstTeamId", match.TeamA.Id},
            {"secondTeamId", match.TeamB.Id},
            {"scoreFirstTeam", match.ScoreA},
            {"scoreSecondTeam", match.ScoreB},
            {"winnerTeamId", match.ScoreA > match.ScoreB ? match.TeamA.Id : match.TeamB.Id},
            {"remainingPuckFirstTeam", match.RemainingPuckA},
            {"remainingPuckSecondTeam", match.RemainingPuckB},
        };

        var res = await httpClient.PostAsync($"/matchs/{match.Id}/finish", new StringContent(JsonSerializer.Serialize(dictionnary, jsonOptions), System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);
    }
}