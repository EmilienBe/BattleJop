namespace BattleJop.Api.Domain.TournamentAggregate;
public class MatchTeam : Aggregate
{
    public MatchTeam()
    {
        
    }

    public int Score { get; set; }

    public bool IsWinner { get; set; }

    public int RemainingPuck { get; set; }

    public Match Match { get; set; }

    public Team Team { get; set; }
}