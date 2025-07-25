namespace BattleJop.Api.Domain.TournamentAggregate;

public record RankTeam(

    Guid TeamId,

    string TeamName,

    int NumberOfVictory,

    int TotalScore,

    int TotalRemainingPuck
);

public record RankTeamWithPosition(

    int Rank,

    Guid TeamId,

    string TeamName,

    int NumberOfVictory,

    int TotalScore,

    int TotalRemainingPuck
);
