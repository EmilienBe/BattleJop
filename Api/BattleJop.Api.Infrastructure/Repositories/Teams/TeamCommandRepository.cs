using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Teams;

public class TeamCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Team>(context), ITeamCommandRepository
{
}
