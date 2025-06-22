namespace BattleJop.Api.Domain.TournamentAggregate;

public class Match : Aggregate
{
    public Match()
    {
        
    }

    public Guid Id { get; private set ; }

    public int RunningOrder { get; private set; }

    public Round Round { get; private set; }

    public ICollection<Team> Teams { get; private set; }

    public Match(Guid id, int runningOrder, Round round)
    {
        ArgumentNullException.ThrowIfNull(round, nameof(round));

        Id = id;
        RunningOrder = runningOrder;
        Round = round;
        Teams = [];
    }

    public void AddTeam(Team team)
    {
        ArgumentNullException.ThrowIfNull(team, nameof(team));
        Teams.Add(team);
    }
}
