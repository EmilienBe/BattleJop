using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories.MatchTeams;

public class MatchTeamCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<MatchTeam>(context), IMatchTeamCommandRepository
{
}
