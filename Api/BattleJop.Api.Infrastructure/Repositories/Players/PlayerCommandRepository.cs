using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Players;

public class PlayerCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Player>(context), IPlayerCommandRepository
{
    public Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        _context.Players.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
}
