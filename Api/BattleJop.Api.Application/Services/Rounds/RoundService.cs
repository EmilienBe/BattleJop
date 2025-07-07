using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories.Rounds;
using BattleJop.Api.Infrastructure.Repositories.Tournaments;

namespace BattleJop.Api.Application.Services.Rounds;

public class RoundService (ITournamentQueryRepository tournamentQueryRepository, IRoundQueryRepository roundQueryRepository) : IRoundService
{
    public async Task<ModelActionResult<Round>> GetRoundByTournamentIdAndId(Guid tournamentId, Guid roundId, CancellationToken cancellationToken)
    {
        var tournament = await tournamentQueryRepository.GetByIdInculeRoundsAsync(tournamentId, cancellationToken);

        if (tournament == null)
            return ModelActionResult<Round>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{tournamentId}' does not exist.");

        var round = await roundQueryRepository.GetRoundByIdIncludeMatchAsync(roundId, cancellationToken);
        if (round == null)
            return ModelActionResult<Round>.Fail(FaultType.ROUND_NOT_FOUND, $"The round with identifier '{roundId}' does not exist.");

        return ModelActionResult<Round>.Ok(round);
    }

    public async Task<ModelActionResult<ICollection<Round>>> GetRoundsByTournamentId(Guid tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await tournamentQueryRepository.GetByIdInculeRoundsAsync(tournamentId, cancellationToken);

        if (tournament == null)
            return ModelActionResult<ICollection<Round>>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{tournamentId}' does not exist.");

        return ModelActionResult<ICollection<Round>>.Ok(tournament.Rounds);
    }
}
