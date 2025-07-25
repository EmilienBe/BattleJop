using BattleJop.Api.Core.ModelActionResult;

namespace BattleJop.Api.Application.Services.Matchs;

public interface IMatchService
{
    Task<ModelActionResult> FinishAsync(Guid matchId, Guid firstTeamId, Guid secondTeamId, int scorefirstTeam, int scoreSecondTeam, int remainingPuckFirstTeam, int remainingPuckSecondTeam, Guid winnerTeamId, CancellationToken cancellationToken);
}
