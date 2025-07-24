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
        async (Guid tournamentId, Guid roundId, IRoundService roundService, CancellationToken cancellationToken) =>
        {
            //Get round
            var result = await roundService.GetRoundByTournamentIdAndId(tournamentId, roundId, cancellationToken);

            return ResolveActionResult(result, result.Result?.ToRoundDetailResponse());
        });

        app.MapGet("tournaments/{tournamentId:guid}/rounds",
        [Tags("Rounds")]
        [ProducesResponseType<ICollection<RoundResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        async (Guid tournamentId, IRoundService roundService, CancellationToken cancellationToken) =>
        {
            //Get rounds
            var result = await roundService.GetRoundsByTournamentId(tournamentId, cancellationToken);

            return ResolveActionResult(result, result.Result?.Select(t => t.ToRoundResponse()).OrderBy(r => r.RunningOrder));
        });
    }
}
