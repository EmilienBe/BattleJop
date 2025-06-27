using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Teams;

public class TeamCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Team>(context), ITeamCommandRepository
{
    public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Teams.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
}
