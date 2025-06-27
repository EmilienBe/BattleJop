namespace BattleJop.Api.Domain.TournamentAggregate;

public class Tournament : Aggregate
{
    public Tournament()
    {
        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public TournamentState State { get; private set; }

    public ICollection<Round> Rounds { get; private set; } = [];

    public ICollection<Team> Teams { get; private set; } = [];

    public Tournament(Guid id, string name)
    {
        Id = id;
        Name = name;
        State = TournamentState.InConfiguration;
        Rounds = [];
        Teams = [];
    }

    public void AddRound(Round round)
    {
        ArgumentNullException.ThrowIfNull(round);
        Rounds.Add(round);
    }

    public void AddTeam(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);

        Teams.Add(team);
    }

    public void UpdateName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public void Start() => State = TournamentState.InProgress;
}

public enum TournamentState
{
    InConfiguration = 0,
    InProgress = 1,
    Finished = 2
}