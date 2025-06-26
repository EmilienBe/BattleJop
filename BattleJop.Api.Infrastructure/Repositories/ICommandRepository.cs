namespace BattleJop.Api.Infrastructure.Repositories;

public interface ICommandRepository<TEntity> where TEntity : class
{
        void Add(TEntity entity);

        void Add(ICollection<TEntity> entities);

        void Update(TEntity entity);

        void Delete(Guid id);

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
