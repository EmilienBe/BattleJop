using BattleJop.Web.Dto;

namespace BattleJop.Web;

public class TournamentApiClient(HttpClient httpClient)
{
    public Task<RoundDto> GetCurrentRound()
    {
        var round = new RoundDto
        {
            RunningOrder = 1
        };

        round.Matches.Add(new MatchDto()
        {
            TeamA = new TeamDto()
            {
                Name = "Les Loulous",
                Players = Tuple.Create("Toto", "Tamo")
            },
            TeamB = new TeamDto()
            {
                Name = "Les Foufous",
                Players = Tuple.Create("Manon", "Marion")
            }
        });
        round.Matches.Add(new MatchDto()
        {
            TeamA = new TeamDto()
            {
                Name = "Les Foufous",
                Players = Tuple.Create("Manon", "Marion")
            },
            TeamB = new TeamDto()
            {
                Name = "Les BG",
                Players = Tuple.Create("Mimi", "Mat")
            }
        });
        round.Matches.Add(new MatchDto()
        {
            TeamA = new TeamDto()
            {
                Name = "Les BG",
                Players = Tuple.Create("Mimi", "Mat")
            },
            TeamB = new TeamDto()
            {
                Name = "Les Loulous",
                Players = Tuple.Create("Toto", "Tamo")
            }
        });
        round.Matches.Add(new MatchDto()
        {
            TeamA = new TeamDto()
            {
                Name = "Les Déchainés",
                Players = Tuple.Create("Tutu", "Tepin")
            },
            TeamB = new TeamDto()
            {
                Name = "Les BG",
                Players = Tuple.Create("Mimi", "Mat")
            }
        });

        return Task.FromResult(round);
    }

    public Task<List<RankingDto>> GetRankings()
    {
        var ranking = new List<RankingDto>
        {
            new() {
                Team = new TeamDto
                {
                    Name = "Les Loulous",
                    Players = Tuple.Create("Toto", "Tamo")
                },
                Wins = 2,
                Losses = 1,
                Pucks = 5,
                Points = 6
            },
            new() {
                Team = new TeamDto
                {
                    Name = "Les Foufous",
                    Players = Tuple.Create("Manon", "Marion")
                },
                Wins = 1,
                Losses = 2,
                Pucks = 3,
                Points = 3
            },
            new() {
                Team = new TeamDto
                {
                    Name = "Les BG",
                    Players = Tuple.Create("Mimi", "Mat")
                },
                Wins = 2,
                Losses = 1,
                Pucks = 4,
                Points = 6
            },
            new() {
                Team = new TeamDto
                {
                    Name = "Les Déchainés",
                    Players = Tuple.Create("Tutu", "Tepin")
                },
                Wins = 0,
                Losses = 3,
                Pucks = 1,
                Points = -10
            }
        }.OrderByDescending(r => r.Wins).ThenByDescending(r => r.Points).ThenByDescending(r => r.Pucks).ToList();
        return Task.FromResult(ranking);
    }
}