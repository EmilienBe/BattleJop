using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;

namespace BattleJop.Api.Application.Services.Teams;

public interface ITeamService
{
    Task<ModelActionResult<ICollection<Team>>> GetTeamsByTournamentId(Guid tournamentId, CancellationToken cancellationToken);
    Task<ModelActionResult<Team>> AddTeamToTournament(Guid tournamentId, string name, List<string> players, CancellationToken cancellationToken);
}
