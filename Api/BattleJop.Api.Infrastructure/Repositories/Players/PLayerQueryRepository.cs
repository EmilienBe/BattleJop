using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Players;

public class PLayerQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Player>(context), IPlayerQueryRepository
{
    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
}
