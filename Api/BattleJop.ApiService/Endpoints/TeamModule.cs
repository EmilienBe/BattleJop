using BattleJop.Api.Application.Services.Teams;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web.Mappers;
using BattleJop.Api.Web.Validator;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BattleJop.Api.Web.Endpoints;

public class TeamModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("tournaments/{tournamentId:guid}/teams",
        [ProducesResponseType<ICollection<TeamResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        async (Guid tournamentId, ITeamService teamService, CancellationToken cancellationToken) =>
        {
            //Get Teams
            var result = await teamService.GetTeamsByTournamentId(tournamentId, cancellationToken);

            if (!result.IsSuccess && result.FaultType == Core.ModelActionResult.FaultType.TOURNAMENT_NOT_FOUND)
                return Results.NotFound(new ErrorResponse(result.FaultType, result.Message));

            return Results.Ok(result.Result.Select(t => t.ToTeamResponse()));
        });

        app.MapPost("tournaments/{tournamentId:guid}/teams",
        [ProducesResponseType<TeamResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        async (Guid tournamentId, AddTeamRequest request, ITeamService teamService, CancellationToken cancellationToken) =>
        {
            //Validation
            var validatorResult = new AddTeamValidator().Validate(request);

            if (!validatorResult.IsValid)
                return Results.BadRequest(validatorResult.Errors);

            //Get Teams
            var result = await teamService.AddTeamToTournament(tournamentId, request.Name, request.Players, cancellationToken);

            if (!result.IsSuccess && result.FaultType == Core.ModelActionResult.FaultType.TOURNAMENT_NOT_FOUND)
                return Results.NotFound(new ErrorResponse(result.FaultType, result.Message));

            return Results.Created($"/tournaments/{tournamentId}/teams/{result.Result.Id}",result.Result.ToTeamResponse());
        });
    }
}