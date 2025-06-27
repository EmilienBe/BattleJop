namespace BattleJop.Api.Domain.TournamentAggregate;

public class Round : Aggregate
{
    public Round()
    {
        
    }

    public int RunningOrder { get; protected set; }

    public Tournament Tournament { get; private set; }

    public ICollection<Match> Matchs { get; private set; }

    public Round (Guid id, int runningOrder, Tournament tournament)
    {
        ArgumentNullException.ThrowIfNull(tournament, nameof(tournament));

        Id = id;
        RunningOrder = runningOrder;
        Tournament = tournament;
        Matchs = [];
    }

    public void AddMatch(Match match)
    {
        ArgumentNullException.ThrowIfNull(match, nameof(match));
        Matchs.Add(match);
    }
}
