using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Application.Services.Rounds;

public interface IRoundService
{
    Task<ModelActionResult<ICollection<Round>>> GetRoundsByTournamentId(Guid tournamentId, CancellationToken cancellationToken);
    Task<ModelActionResult<Round>> GetRoundByTournamentIdAndId(Guid tournamentId, Guid roundId, CancellationToken cancellationToken);
}
