using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Matchs;

public class MatchCommandRepository : AbstractCommandRepository<Match>, IMatchCommandRepository
{
    public MatchCommandRepository(BattleJopDbContext context) : base(context)
    {
    }

    public Task<Match?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _context.Matchs.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

}
