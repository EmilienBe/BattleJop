namespace BattleJop.Api.Application.Services.Tournament;

public interface ITournamentService
{
    Task<Domain.TournamentAggregate.Tournament> AddAsync(string name, int numberOfRounds, CancellationToken cancellationToken);
    Task<Domain.TournamentAggregate.Tournament> UpdateAsync(string name, CancellationToken cancellationToken);
    Task<ICollection<Domain.TournamentAggregate.Tournament>> GetAllAsync(CancellationToken cancellationToken);
    Task<Domain.TournamentAggregate.Tournament> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task StartAsync(Guid id, CancellationToken cancellationToken);
}
