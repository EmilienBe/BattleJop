using BattleJop.Api.Application.Services.Rounds;
using BattleJop.Api.Web.Dtos;
using BattleJop.Api.Web.Mappers;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BattleJop.Api.Web.Endpoints;

public class RoundModule : AbstractModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("tournaments/{tournamentId:guid}/rounds/{roundId}",
        [Tags("Rounds")]
        [ProducesResponseType<RoundDetailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("Returns details of a round and its matchs.")]
        async (Guid tournamentId, Guid roundId, IRoundService roundService, CancellationToken cancellationToken) =>
        {
            //Get round
            var result = await roundService.GetRoundByTournamentIdAndIdAsync(tournamentId, roundId, cancellationToken);

            return ResolveActionResult(result, result.Result?.ToRoundDetailResponse());
        });

        app.MapGet("tournaments/{tournamentId:guid}/rounds",
        [Tags("Rounds")]
        [ProducesResponseType<ICollection<RoundResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("Returns the tournament rounds.")]
        async (Guid tournamentId, IRoundService roundService, CancellationToken cancellationToken) =>
        {
            //Get rounds
            var result = await roundService.GetRoundsByTournamentIdAsync(tournamentId, cancellationToken);

            return ResolveActionResult(result, result.Result?.Select(t => t.ToRoundResponse()).OrderBy(r => r.RunningOrder));
        });
    }
}
