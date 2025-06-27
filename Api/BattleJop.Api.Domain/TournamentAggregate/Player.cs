namespace BattleJop.Api.Domain.TournamentAggregate;

public class Player : Aggregate
{
    public Player()
    {
        
    }

    public string Name { get; private set; }

    public Team Team { get; private set; }

    public Player(Guid id, string name, Team team)
    {
        ArgumentNullException.ThrowIfNull(team, nameof(team));  

        Id = id;
        Name = name;
        Team = team;
    }
}