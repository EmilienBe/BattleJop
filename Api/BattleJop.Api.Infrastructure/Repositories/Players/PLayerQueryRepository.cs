using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Players;

public class PlayerQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Player>(context), IPlayerQueryRepository
{

}
