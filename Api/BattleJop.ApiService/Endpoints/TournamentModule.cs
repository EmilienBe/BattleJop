using BattleJop.Api.Application.Services.Tournaments;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web.Mappers;
using BattleJop.Api.Web.Validator;
using Carter;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BattleJop.Api.Web.Endpoints;

public class TournamentModule : AbstractModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("tournaments",
        [Tags("Tournaments")]
        [ProducesResponseType<TournamentResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<List<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        [EndpointDescription("Add a tournament.")]
        async (AddTournamentRequest request, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Validation
            var validatorResult = new AddTournamentValidator().Validate(request);

            if (!validatorResult.IsValid)
                return Results.BadRequest(validatorResult.Errors);

            //Add Tournament
            var result = await tournamentService.AddAsync(request.Name, request.NumberRounds, cancellationToken);

            return ResolveActionResult(result, result.Result.ToTournamentResponse(), $"tournaments/{result.Result?.Id}");
        });

        app.MapGet("tournaments/{tournamentId:guid}",
        [Tags("Tournaments")]
        [ProducesResponseType<TournamentResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("Returns the details of a tournament.")]
        async (Guid tournamentId, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Tournament
            var result = await tournamentService.GetByIdAsync(tournamentId, cancellationToken);

            return ResolveActionResult(result, result.Result?.ToTournamentResponse());
        });

        app.MapGet("tournaments",
        [Tags("Tournaments")]
        [ProducesResponseType<ICollection<TournamentResponse>>(StatusCodes.Status200OK)]
        [EndpointDescription("Returns tournaments.")]
        async (ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Tournaments
            var result = await tournamentService.GetAllAsync(cancellationToken);

            return ResolveActionResult(result, result.Result?.Select(t => t.ToTournamentResponse()));
        });

        app.MapPost("tournaments/{tournamentId:guid}/start",
        [Tags("Tournaments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("Start of a tournament.")]
        async (Guid tournamentId, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Start Tournament
            var result = await tournamentService.StartAsync(tournamentId, cancellationToken);

            return ResolveActionResult(result);
        });

        app.MapGet("tournaments/{tournamentId:guid}/ranking",
        [Tags("Tournaments")]
        [ProducesResponseType<ICollection<RankingResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("Returns the live ranking of a tournament.")]
        async (Guid tournamentId, ITournamentService tournamentService, CancellationToken cancellationToken) =>
        {
            //Get Ranking
            var result = await tournamentService.GetRanking(tournamentId, cancellationToken);

            return ResolveActionResult(result, result.Result?.Select(r => r.ToRankingResponse()));
        });
    }
}
