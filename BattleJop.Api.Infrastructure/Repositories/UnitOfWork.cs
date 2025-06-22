using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;

namespace BattleJop.Api.Infrastructure.Repositories;

public class UnitOfWork : IDisposable
{
    private BattleJopCommandDbContext context = new BattleJopCommandDbContext();
    private GenericRepository<Tournament> tournamentRepository;
    private GenericRepository<Round> roundRepository;

    public GenericRepository<Tournament> TournamentRepository =>
        tournamentRepository ??= new GenericRepository<Tournament>(context);

    public GenericRepository<Round> RoundRepository =>
        roundRepository ??= new GenericRepository<Round>(context);

    public void Save() => 
        context.SaveChanges();

    public Task SaveAsync() => 
        context.SaveChangesAsync();

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
