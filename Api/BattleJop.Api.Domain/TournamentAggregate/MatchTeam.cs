namespace BattleJop.Api.Domain.TournamentAggregate;
public class MatchTeam : Aggregate
{
    public MatchTeam()
    {
        
    }

    public MatchTeam(Guid id, Match match, Team team)
    {
        Id = id;
        Match = match;
        Team = team;
    }

    public void UpdateScore(int score, int remainingPuck, bool isWinner)
    {
        Score = score;
        RemainingPuck = remainingPuck;
        IsWinner = isWinner;
    }

    public int Score { get; set; }

    public bool IsWinner { get; set; }

    public int RemainingPuck { get; set; }

    public Match Match { get; set; }

    public Team Team { get; set; }
}