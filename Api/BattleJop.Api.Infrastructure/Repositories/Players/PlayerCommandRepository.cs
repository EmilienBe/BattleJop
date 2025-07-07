using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Players;

public class PlayerCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Player>(context), IPlayerCommandRepository
{

}
