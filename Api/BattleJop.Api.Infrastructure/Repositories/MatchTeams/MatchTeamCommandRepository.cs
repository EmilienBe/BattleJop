using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.MatchTeams;

public class MatchTeamCommandRepository : AbstractCommandRepository<MatchTeam>, IMatchTeamCommandRepository
{
    public MatchTeamCommandRepository(BattleJopDbContext context) : base(context)
    {
    }

    public Task<MatchTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _context.MatchTeams.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
}
