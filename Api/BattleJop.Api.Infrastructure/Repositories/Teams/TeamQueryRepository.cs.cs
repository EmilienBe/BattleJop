using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Teams;

public class TeamQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Team>(context), ITeamQueryRepository
{
}
