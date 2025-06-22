using MediatR;

namespace BattleJop.Api.Application.Tournament.Create;

public record CreateTournamentCommand(string Name, int NumberOfRounds) : IRequest;

