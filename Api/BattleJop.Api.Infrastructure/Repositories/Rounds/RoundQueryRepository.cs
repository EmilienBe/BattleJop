using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Rounds;

public class RoundQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Round>(context), IRoundQueryRepository
{
    public async Task<Round?> GetRoundByIdIncludeMatchAsync(Guid id, CancellationToken cancellationToken) => 
        await _dbSet
        .Include(r => r.Matchs)
        .ThenInclude(m => m.Scores)
        .ThenInclude(s => s.Team)
        .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
}
