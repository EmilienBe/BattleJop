using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Infrastructure.Repositories.Players;
using BattleJop.Api.Infrastructure.Repositories.Teams;
using BattleJop.Api.Infrastructure.Repositories.Tournaments;

namespace BattleJop.Api.Application.Services.Teams;

public class TeamService : ITeamService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITournamentCommandRepository _tournamentCommandRepository;
    private readonly ITournamentQueryRepository _tournamentQueryRepository;
    private readonly ITeamCommandRepository _teamCommandRepository;
    private readonly IPlayerCommandRepository _playerCommandRepository;

    public TeamService(IUnitOfWork unitOfWork,
        ITournamentCommandRepository tournamentCommandRepository,
        ITournamentQueryRepository tournamentQueryRepository,
        ITeamCommandRepository teamCommandRepository,
        IPlayerCommandRepository playerCommandRepository)
    {
        _unitOfWork = unitOfWork;
        _tournamentCommandRepository = tournamentCommandRepository;
        _tournamentQueryRepository = tournamentQueryRepository;
        _teamCommandRepository = teamCommandRepository;
        _playerCommandRepository = playerCommandRepository;
    }

    public async Task<ModelActionResult<Team>> AddTeamToTournament(Guid tournamentId, string name, List<string> playerNames, CancellationToken cancellationToken)
    {
        var tournament = await _tournamentCommandRepository.GetByIdAsync(tournamentId, cancellationToken);

        if (tournament == null)
            return ModelActionResult<Team>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{tournamentId}' does not exist.");

        if (!tournament.IsInConfiguration())
            return ModelActionResult<Team>.Fail(FaultType.TOURNAMENT_INVALID_STATE, $"The tournament is in state '{tournament.State}', impossible to add a new team.");

        var team = new Team(Guid.NewGuid(), name, tournament);

        foreach (var player in playerNames)
            team.AddPlayer(new Player(Guid.NewGuid(), player, team));

        _teamCommandRepository.Add(team);
        _playerCommandRepository.Add(team.Players);

        await _unitOfWork.SaveChangesAsync();

        return ModelActionResult<Team>.Created(team);
    }

    public async Task<ModelActionResult<ICollection<Team>>> GetTeamsByTournamentId(Guid tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await _tournamentQueryRepository.GetByIdInculeTeamAndPlayerAsync(tournamentId, cancellationToken);

        if (tournament == null)
            return ModelActionResult<ICollection<Team>>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{tournamentId}' does not exist.");

        return ModelActionResult<ICollection<Team>>.Ok(tournament.Teams);
    }

    public async Task<ModelActionResult<Team>> GetTeamByTournamentIdAndId(Guid tournamentId, Guid teamId, CancellationToken cancellationToken)
    {
        var tournament = await _tournamentQueryRepository.GetByIdInculeTeamAndPlayerAsync(tournamentId, cancellationToken);

        if (tournament == null)
            return ModelActionResult<Team>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{tournamentId}' does not exist.");

        var team = tournament.Teams.FirstOrDefault(t => t.Id == teamId);
        if (team == null)
            return ModelActionResult<Team>.Fail(FaultType.TEAM_NOT_FOUND, $"The team with identifier '{teamId}' does not exist.");

        return ModelActionResult<Team>.Ok(team);
    }
}
