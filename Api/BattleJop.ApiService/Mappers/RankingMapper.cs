using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;

namespace BattleJop.Api.Web.Mappers;

public static class RankingMapper
{
    public static RankingResponse ToRankingResponse(this RankTeamWithPosition source) => new RankingResponse 
    {
        Rank = source.Rank,
        TeamId = source.TeamId,
        TeamName = source.TeamName,
        NumberOfVictory = source.NumberOfVictory,
        TotalScore = source.TotalScore,
        TotalRemainingPuck = source.TotalRemainingPuck 
    };
}
