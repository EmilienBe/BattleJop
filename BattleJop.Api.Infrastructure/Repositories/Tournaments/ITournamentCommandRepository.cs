using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public interface ITournamentCommandRepository : ICommandRepository<Tournament>
{
}
