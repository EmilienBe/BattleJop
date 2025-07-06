using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Infrastructure.Repositories.Tournaments;
using BattleJop.Api.Infrastructure.Repositories.Matchs;
using BattleJop.Api.Infrastructure.Repositories.MatchTeams;

namespace BattleJop.Api.Application.Services.Tournaments;

public class TournamentService(IUnitOfWork unitOfWork, ITournamentCommandRepository tournamentCommandRepository, ITournamentQueryRepository tournamentQueryRepository, IMatchCommandRepository matchCommandRepository, IMatchTeamCommandRepository matchTeamCommandRepository) : ITournamentService
{




    public async Task<ModelActionResult<Tournament>> AddAsync(string name, int numberOfRounds, CancellationToken cancellationToken)
    {
        var tournament = new Tournament(Guid.NewGuid(), name);

        for (int runningOrder = 1; runningOrder < numberOfRounds + 1; runningOrder++)
            tournament.AddRound(new Round(Guid.NewGuid(), runningOrder, tournament));

        tournamentCommandRepository.Add(tournament);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult<Tournament>.Created(tournament); ;
    }

    public async Task<ModelActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var tournament = await tournamentCommandRepository.GetByIdAsync(id, cancellationToken);
        if (tournament == null)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{id}' does not exist.");

        tournamentCommandRepository.Delete(tournament.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult.Ok();
    }

    public async Task<ModelActionResult<ICollection<Tournament>>> GetAllAsync(CancellationToken cancellationToken) =>
                ModelActionResult<ICollection<Tournament>>.Ok(await tournamentQueryRepository.GetAllAsync(cancellationToken));

    public async Task<ModelActionResult<Tournament>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var tournament = await tournamentCommandRepository.GetByIdAsync(id, cancellationToken);

        if (tournament == null)
            return ModelActionResult<Tournament>.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{id}' does not exist.");

        return ModelActionResult<Tournament>.Ok(tournament);
    }

    public async Task<ModelActionResult> StartAsync(Guid id, CancellationToken cancellationToken)
    {
        var tournament = await tournamentCommandRepository.GetByIdInculeTeamAndRoundAsync(id, cancellationToken);

        if (tournament is null)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_NOT_FOUND, $"The tournament with identifier '{id}' does not exist.");

        if (tournament.State != TournamentState.InConfiguration)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_INVALID_STATE, $"The tournament is in state '{tournament.State}', which is invalid for starting.");

        if (tournament.Teams.Count < 2)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_INVALID_NUMBER_TEAMS, "The tournament must start with a minimum of two teams.");

        if (tournament.Teams.Count % 2 != 0)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_INVALID_NUMBER_TEAMS, "The tournament must start with an even number of teams.");

        var firstRound = tournament.Rounds.FirstOrDefault();
        if (firstRound is null)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_NO_ROUND_EXIST, "The tournament has no rounds.");

        var result = tournament.GenerateInitialMatches(firstRound);

        tournament.UpdateState(TournamentState.InProgress);
  
        tournamentCommandRepository.Update(tournament);
        matchCommandRepository.Add(result.matches);
        matchTeamCommandRepository.Add(result.matchTeams);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult.Ok();
    }

    public async Task<ModelActionResult<Tournament>> UpdateAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

