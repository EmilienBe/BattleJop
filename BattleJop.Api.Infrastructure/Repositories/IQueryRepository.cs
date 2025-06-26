namespace BattleJop.Api.Infrastructure.Repositories;

public interface IQueryRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}
