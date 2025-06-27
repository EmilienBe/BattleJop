using BattleJop.Api.Application.Services.Tournaments;
using BattleJop.Api.Core.ModelActionResult;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web.Mappers;
using BattleJop.Api.Web.Validator;
using Carter;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BattleJop.Api.Web.Endpoints;

public class TournamentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("tournaments",
        [ProducesResponseType<TournamentResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<List<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        async (AddTournamentRequest request, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Validation
            var validatorResult = new AddTournamentValidator().Validate(request);

            if (!validatorResult.IsValid)
                return Results.BadRequest(validatorResult.Errors);

            //Add Tournament
            var result = await tournamentService.AddAsync(request.Name, request.NumberRounds, cancellationToken);

            return Results.Created($"/tournaments/{result.Result.Id}", result.Result.ToTournamentResponse());
        });

        app.MapGet("tournaments/{id:guid}",
        [ProducesResponseType<TournamentResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        async (Guid id, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Tournament
            var result = await tournamentService.GetByIdAsync(id, cancellationToken);

            if (!result.IsSuccess && result.FaultType == FaultType.TOURNAMENT_NOT_FOUND)
                return Results.NotFound(new ErrorResponse(result.FaultType, result.Message));

            return Results.Ok(result.Result.ToTournamentResponse());
        });

        app.MapGet("tournaments",
        [ProducesResponseType<TournamentResponse>(StatusCodes.Status200OK)]
        async (ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Tournaments
            var result = await tournamentService.GetAllAsync(cancellationToken);

            return Results.Ok(result.Result.Select(t => t.ToTournamentResponse()));
        });
    }
}
