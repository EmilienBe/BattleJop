using BattleJop.Api.Application.Services.Tournament;
using BattleJop.Api.Web.Dtos.Tournament;
using Carter;

namespace BattleJop.Api.Web.Endpoints;

public class Tournaments : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("tournaments", async (AddTournamentRequest request, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            var tournamentId = tournamentService.AddAsync(request.Name, request.NumberRounds, cancellationToken);

            return Results.Created($"/todoitems/{tournamentId}", tournamentId);
        });
    }
}
