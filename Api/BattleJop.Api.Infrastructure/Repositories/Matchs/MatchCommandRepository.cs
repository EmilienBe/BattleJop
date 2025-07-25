using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Matchs;

public class MatchCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Match>(context), IMatchCommandRepository
{
    public async Task<Match?> GetByIdIncludeScoresAndTeamsAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Matchs
        .Include(m => m.Scores)
        .ThenInclude(s => s.Team)
        .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
}
