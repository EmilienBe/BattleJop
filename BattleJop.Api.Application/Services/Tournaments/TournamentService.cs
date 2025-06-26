using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Infrastructure.Repositories.Tournaments;
using System.Collections.Generic;
using System.Threading;

namespace BattleJop.Api.Application.Services.Tournaments;

public class TournamentService(IUnitOfWork unitOfWork, ITournamentRepository tournamentRepository) : ITournamentService
{
    public async Task<ModelActionResult<Tournament>> AddAsync(string name, int numberOfRounds, CancellationToken cancellationToken)
    {
        var tournament = new Tournament(Guid.NewGuid(), name);

        for (int runningOrder = 1; runningOrder < numberOfRounds + 1; runningOrder++)
            tournament.AddRound(new Round(Guid.NewGuid(), runningOrder, tournament));

        tournamentRepository.Add(tournament);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult<Tournament>.Ok(tournament); ;
    }

    public async Task<ModelActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var tournament = await tournamentRepository.GetByIdAsync(id, cancellationToken);
        if (tournament == null)
            return ModelActionResult.Fail(FaultType.TOURNAMENT_NOT_FOUND);

        tournamentRepository.Delete(tournament.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ModelActionResult.Ok();
    }

    public async Task<ModelActionResult<ICollection<Tournament>>> GetAllAsync(CancellationToken cancellationToken) =>
                ModelActionResult<ICollection<Tournament>>.Ok(await tournamentRepository.GetAllAsync(cancellationToken));

    public async Task<ModelActionResult<Tournament>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ModelActionResult> StartAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ModelActionResult<Tournament>> UpdateAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

