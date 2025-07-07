using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Infrastructure.Repositories.Rounds;

public interface IRoundQueryRepository : IQueryRepository<Round>
{
    Task<Round?> GetRoundByIdIncludeMatchAsync(Guid id, CancellationToken cancellationToken);
}
