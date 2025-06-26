using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories;

public abstract class AbstractQueryRepository<TEntity> where TEntity : class
{
    protected readonly BattleJopCommandDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public AbstractQueryRepository(BattleJopCommandDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
}