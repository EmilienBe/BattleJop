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
}