using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories;

public abstract class AbstractCommandRepository<TEntity> where TEntity : class
{
    protected readonly BattleJopDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public AbstractCommandRepository(BattleJopDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity) => 
        _dbSet.Add(entity);

    public void Add(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
            Add(entity);
    }

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
}