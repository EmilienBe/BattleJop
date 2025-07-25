using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Infrastructure.Repositories.Matchs;

namespace BattleJop.Api.Application.Services.Matchs;

public class MatchService(IUnitOfWork unitOfWork, IMatchCommandRepository matchCommandRepository) : IMatchService
{
    public async Task<ModelActionResult> FinishAsync(Guid matchId, Guid firstTeamId, Guid secondTeamId, int scoreFirstTeam, int scoreSecondTeam, int remainingPuckFirstTeam, int remainingPuckSecondTeam, Guid winnerTeamId, CancellationToken cancellationToken)
    {
        var match = await matchCommandRepository.GetByIdIncludeScoresAndTeamsAsync(matchId, cancellationToken);

        if (match == null)
            return ModelActionResult.Fail(FaultType.MATCH_NOT_FOUND, $"The match with identifier '{matchId}' does not exist.");

        if (match.IsFinish())
            return ModelActionResult.Fail(FaultType.MATCH_NOT_FOUND, $"The match with identifier '{matchId}' is already finished.");

        var teamsIds = match.Scores.Select(s => s.Team).Select(t => t.Id);

        if (!teamsIds.Contains(firstTeamId))
            return ModelActionResult.Fail(FaultType.TEAM_NOT_FOUND_IN_MATCH, $"The team with the ID '{firstTeamId}' is not part of the match.");

        if (!teamsIds.Contains(secondTeamId))
            return ModelActionResult.Fail(FaultType.TEAM_NOT_FOUND_IN_MATCH, $"The team with the ID '{secondTeamId}' is not part of the match.");

        var matchScoreFirstTeam = match.Scores.First(s => s.Team.Id == firstTeamId);
        var matchScoreSecondTeam = match.Scores.First(s => s.Team.Id == secondTeamId);

        matchScoreFirstTeam.UpdateScore(scoreFirstTeam, remainingPuckFirstTeam, firstTeamId == winnerTeamId);
        matchScoreSecondTeam.UpdateScore(scoreSecondTeam, remainingPuckSecondTeam, secondTeamId == winnerTeamId);

        match.Finish();

        matchCommandRepository.Update(match);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult.Ok();
    }
}
