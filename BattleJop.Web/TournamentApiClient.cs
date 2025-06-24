using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Web;

public class TournamentApiClient(HttpClient httpClient)
{
    private static readonly Tournament _tournament = new(Guid.NewGuid(), "");

    public Task<Round> GetCurrentRound()
    {
        var round = new Round(Guid.NewGuid(), 1, _tournament);

        round.AddMatch(new Match(Guid.NewGuid(), 1, new Round())
        {
            Teams =
            {
                new Team(Guid.NewGuid(), "Les Foufous", _tournament),
                new Team(Guid.NewGuid(), "Les Loulous", _tournament)
            }
        });
        round.AddMatch(new Match(Guid.NewGuid(), 1, new Round())
            {
                Teams =
                {
                    new Team(Guid.NewGuid(), "Le Palet Fou", _tournament),
                    new Team(Guid.NewGuid(), "Le Palet Mou", _tournament)
                }
            }
        );
        round.AddMatch(new Match(Guid.NewGuid(), 1, new Round())
        {
            Teams =
            {
                new Team(Guid.NewGuid(), "Les Foufous", _tournament),
                new Team(Guid.NewGuid(), "Les Loulous", _tournament)
            }
        });
        round.AddMatch(new Match(Guid.NewGuid(), 1, new Round())
        {
            Teams =
            {
                new Team(Guid.NewGuid(), "Les Foufous", _tournament),
                new Team(Guid.NewGuid(), "Les Loulous", _tournament)
            }
        });

        return Task.FromResult(round);
    }
}