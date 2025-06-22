using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Infrastructure.Repositories;
using MediatR;

namespace BattleJop.Api.Application.Tournament.Create;

internal class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand>
{
    private readonly UnitOfWork _unitOfWork;
    public CreateTournamentCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        var tournament = new Domain.TournamentAggregate.Tournament(Guid.NewGuid(), request.Name);

        for (int runningOrder = 1; runningOrder < request.NumberOfRounds + 1; runningOrder++)
            tournament.AddRound(new Round(Guid.NewGuid(), runningOrder, tournament));

        _unitOfWork.TournamentRepository.Insert(tournament);
        await _unitOfWork.SaveAsync();
    }
}
