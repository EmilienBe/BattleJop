using BattleJop.Api.Application.Services.Tournaments;
using BattleJop.Api.Web.Dtos.AddTournament;
using BattleJop.Api.Web.Extensions;
using BattleJop.Api.Web.Validator;
using Carter;

namespace BattleJop.Api.Web.Endpoints;

public class Tournaments : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("tournaments", async (AddTournamentRequest request, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Validation
            var validatorResult = new AddTournamentValidator().Validate(request);

            if (!validatorResult.IsValid)
                return Results.BadRequest(validatorResult.Errors);

            //Add Tournament
            var result = await tournamentService.AddAsync(request.Name, request.NumberRounds, cancellationToken);

            return Results.Created($"/tournaments/{result.Result.Id}", result.Result.ToAddTournamentresponse());
        });

        app.MapGet("tournaments", async (ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Tournaments
            var result = await tournamentService.GetAllAsync(cancellationToken);

            return Results.Ok(result.Result.Select(t => t.ToAddTournamentresponse()));
        });
    }
}
