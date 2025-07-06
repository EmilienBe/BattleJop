using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public interface ITournamentCommandRepository : ICommandRepository<Tournament>
{
    Task<Tournament?> GetByIdInculeTeamAndRoundAsync(Guid tournamentId, CancellationToken cancellationToken);
}
