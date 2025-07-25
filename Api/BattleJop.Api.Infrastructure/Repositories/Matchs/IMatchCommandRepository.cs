using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Infrastructure.Repositories.Matchs;

public interface IMatchCommandRepository : ICommandRepository<Match>
{
    Task<Match?> GetByIdIncludeScoresAndTeamsAsync(Guid id, CancellationToken cancellationToken);
}
