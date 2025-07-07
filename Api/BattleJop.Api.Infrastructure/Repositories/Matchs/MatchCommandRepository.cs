using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.Matchs;

public class MatchCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Match>(context), IMatchCommandRepository
{
}
