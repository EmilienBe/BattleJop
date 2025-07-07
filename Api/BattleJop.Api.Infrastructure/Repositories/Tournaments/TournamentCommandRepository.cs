using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public class TournamentCommandRepository(BattleJopDbContext context) : AbstractCommandRepository<Tournament>(context), ITournamentCommandRepository
{
    public async Task<Tournament?> GetByIdInculeTeamAndRoundAsync(Guid tournamentId, CancellationToken cancellationToken) =>
        await _context.Tournaments
        .Include(t => t.Teams)
        .Include(t => t.Rounds)
        .FirstOrDefaultAsync(t => t.Id == tournamentId, cancellationToken);
}
