using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Infrastructure.Repositories.Tournament;

namespace BattleJop.Api.Application.Services.Tournament;

public class TournamentService : ITournamentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITournamentRepository _tournamentRepository;
    public TournamentService(IUnitOfWork unitOfWork, ITournamentRepository tournamentRepository)
    {
        _unitOfWork = unitOfWork;
        _tournamentRepository = tournamentRepository;
    }

    public async Task<Domain.TournamentAggregate.Tournament> AddAsync(string name, int numberOfRounds, CancellationToken cancellationToken)
    {
        var tournament = new Domain.TournamentAggregate.Tournament(Guid.NewGuid(), name);

        for (int runningOrder = 1; runningOrder < numberOfRounds + 1; runningOrder++)
            tournament.AddRound(new Round(Guid.NewGuid(), runningOrder, tournament));

        _tournamentRepository.Add(tournament);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tournament;
    }

    public async Task DeleteByIdAsync(Guid id , CancellationToken cancellationToken)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id, cancellationToken);
        if (tournament == null)
            throw new NotImplementedException();

        _tournamentRepository.Delete(tournament.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<ICollection<Domain.TournamentAggregate.Tournament>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.TournamentAggregate.Tournament> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.TournamentAggregate.Tournament> UpdateAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

