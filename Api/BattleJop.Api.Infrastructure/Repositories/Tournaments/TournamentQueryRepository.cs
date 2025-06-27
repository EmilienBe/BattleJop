using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public class TournamentQueryRepository(BattleJopCommandDbContext context) : AbstractQueryRepository<Tournament>(context), ITournamentQueryRepository
{
    public async Task<Tournament?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<Tournament?> GetByIdInculeTeamAndPlayerAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbSet
        .AsNoTracking()
        .Include(t => t.Teams)
        .ThenInclude(t => t.Players)
        .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
}
