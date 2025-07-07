using BattleJop.Api.Domain;
using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories;

public abstract class AbstractQueryRepository<TEntity> where TEntity : Aggregate
{
    protected readonly BattleJopDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public AbstractQueryRepository(BattleJopDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}