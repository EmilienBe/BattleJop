using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories;

public class UnitOfWork(BattleJopDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => 
        context.SaveChangesAsync(cancellationToken);
}
