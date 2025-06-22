using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Tournament;

public class TournamentRepository : AbstractRepository<Domain.TournamentAggregate.Tournament>, ITournamentRepository
{
    public TournamentRepository(BattleJopCommandDbContext context) : base(context)
    {
    }

    public async Task<Domain.TournamentAggregate.Tournament?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
}
