using BattleJop.Api.Application.Tournament.Create;
using Carter;
using MediatR;

namespace BattleJop.Api.Web.Endpoints;

public class Tournaments : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("tournaments", async (CreateTournamentCommand command, ISender sender) => {
            await sender.Send(command);

            return Results.Ok();
        });
    }
}
