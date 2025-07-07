using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;

namespace BattleJop.Api.Web.Mappers;

public static class RoundMapper
{
    public static RoundResponse ToRoundResponse(this Round source) => new RoundResponse { Id = source.Id, RunningOrder = source.RunningOrder, State = source.State };

    public static RoundDetailResponse ToRoundDetailResponse(this Round source)
    {
        return new RoundDetailResponse
        {
            Id = source.Id,
            RunningOrder = source.RunningOrder,
            State = source.State,
            Matchs = [.. source.Matchs.Select(m => m.ToMatchResponse()).OrderBy(m => m.RunningOrder)]
        };
    }

    private static MatchResponse ToMatchResponse(this Match source)
    {
        return new MatchResponse 
        {
            Id = source.Id,
            RunningOrder = source.RunningOrder,
            State = source.State,
            FirstTeamScore = source.Scores.First().ToTeamScoreResponse(),
            SecondTeamScore = source.Scores.Last().ToTeamScoreResponse()
        };
    }

    private static TeamScoreResponse ToTeamScoreResponse(this MatchTeam source)
    {
        return new TeamScoreResponse 
        {
            TeamId = source.Team.Id,
            TeamName = source.Team.Name,
            Score = source.Score,
            IsWinner = source.IsWinner,
            RemainingPuck = source.RemainingPuck
        };
    }
}
