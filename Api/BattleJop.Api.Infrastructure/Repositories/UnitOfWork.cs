using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories;

public class UnitOfWork(BattleJopCommandDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => 
        context.SaveChangesAsync(cancellationToken);
}
