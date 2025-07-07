using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Rounds;

public class RoundCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Round>(context), IRoundCommandRepository
{
}
