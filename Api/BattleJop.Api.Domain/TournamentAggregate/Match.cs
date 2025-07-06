namespace BattleJop.Api.Domain.TournamentAggregate;
public class Match : Aggregate
{
    public Match()
    {
        
    }

    public int RunningOrder { get; private set; }

    public Round Round { get; private set; }

    public ICollection<MatchTeam> Scores { get; private set; }

    public Match(Guid id, int runningOrder, Round round)
    {
        ArgumentNullException.ThrowIfNull(round, nameof(round));

        Id = id;
        RunningOrder = runningOrder;
        Round = round;
        Scores = [];
    }

    public void AddScoreTeam(MatchTeam scrore)
    {
        ArgumentNullException.ThrowIfNull(scrore, nameof(scrore));
        Scores.Add(scrore);
    }
}
