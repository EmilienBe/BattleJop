using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Teams;

public class TeamQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Team>(context), ITeamQueryRepository
{
    public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Teams.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
}
