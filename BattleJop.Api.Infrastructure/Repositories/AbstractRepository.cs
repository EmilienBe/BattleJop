using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories;

public abstract class AbstractRepository<TEntity> where TEntity : class
{
    protected readonly BattleJopCommandDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public AbstractRepository(BattleJopCommandDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity) => 
        _dbSet.Add(entity);

    public void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public void Delete(Guid id)
    {
        TEntity? entityToDelete = _dbSet.Find(id);
        if (entityToDelete != null)
            Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
            _dbSet.Attach(entityToDelete);

        _dbSet.Remove(entityToDelete);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken) => 
        await _dbSet.ToListAsync(cancellationToken).ConfigureAwait(false);
}
