using BattleJop.Api.Application.Services.Tournament;
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
            var validator = new AddTournamentValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
                return Results.BadRequest(result.Errors);

            //Add Tournament
            var tournament = await tournamentService.AddAsync(request.Name, request.NumberRounds, cancellationToken);

            var response = tournament.ToAddTournamentresponse();

            return Results.Created($"/tournaments/{response.Id}", response);
        });
    }
}
