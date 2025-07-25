using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Application.Services.Teams;

public interface ITeamService
{
    Task<ModelActionResult<ICollection<Team>>> GetTeamsByTournamentIdAsync(Guid tournamentId, CancellationToken cancellationToken);
    Task<ModelActionResult<Team>> GetTeamByTournamentIdAndIdAsync(Guid tournamentId, Guid teamId, CancellationToken cancellationToken);
    Task<ModelActionResult<Team>> AddTeamToTournamentAsync(Guid tournamentId, string name, List<string> players, CancellationToken cancellationToken);
}
