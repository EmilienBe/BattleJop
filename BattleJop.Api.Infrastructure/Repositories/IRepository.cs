namespace BattleJop.Api.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid id);

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}
