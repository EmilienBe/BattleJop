using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Application.Services.Tournaments;

public interface ITournamentService
{
    Task<ModelActionResult<Tournament>> AddAsync(string name, int numberOfRounds, CancellationToken cancellationToken);
    Task<ModelActionResult<ICollection<Tournament>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ModelActionResult<Tournament>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ModelActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ModelActionResult> StartAsync(Guid id, CancellationToken cancellationToken);
    Task<ModelActionResult<ICollection<RankTeamWithPosition>>> GetRanking(Guid id, CancellationToken cancellationToken);
}
