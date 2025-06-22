namespace BattleJop.Api.Domain.TournamentAggregate;

public class MatchTeam
{
    public MatchTeam()
    {
        
    }

    public int Score { get; set; }

    public bool IsWinner { get; set; }

    public int RemainingPuck { get; set; }

    public Guid MatchId { get; set; }

    public Guid TeamId { get; set; }
}