using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace BattleJop.Api.Infrastructure.Repositories.Tournaments;

public class TournamentQueryRepository(BattleJopDbContext context) : AbstractQueryRepository<Tournament>(context), ITournamentQueryRepository
{
    public async Task<Tournament?> GetByIdInculeRoundsAsync(Guid tournamentId, CancellationToken cancellationToken) =>
        await _dbSet
        .AsNoTracking()
        .Include(t => t.Rounds)
        .FirstOrDefaultAsync(t => t.Id == tournamentId, cancellationToken);

    public async Task<Tournament?> GetByIdInculeScoresAsync(Guid tournamentId, CancellationToken cancellationToken) =>
        await _dbSet
        .AsNoTracking()
        .Include(t => t.Teams)
        .ThenInclude(t => t.Scores)
        .FirstOrDefaultAsync(t => t.Id == tournamentId, cancellationToken);

    public async Task<Tournament?> GetByIdInculeTeamAndPlayerAsync(Guid tournamentId, CancellationToken cancellationToken) =>
        await _dbSet
        .AsNoTracking()
        .Include(t => t.Teams)
        .ThenInclude(t => t.Players)
        .FirstOrDefaultAsync(t => t.Id == tournamentId, cancellationToken);
}
