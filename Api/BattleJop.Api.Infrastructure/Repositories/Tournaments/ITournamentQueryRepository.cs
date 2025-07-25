using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public interface ITournamentQueryRepository : IQueryRepository<Tournament>
{
    Task<Tournament?> GetByIdInculeTeamAndPlayerAsync(Guid tournamentId, CancellationToken cancellationToken);
    Task<Tournament?> GetByIdInculeRoundsAsync(Guid tournamentId, CancellationToken cancellationToken);
    Task<Tournament?> GetByIdInculeScoresAsync(Guid tournamentId, CancellationToken cancellationToken);
}
