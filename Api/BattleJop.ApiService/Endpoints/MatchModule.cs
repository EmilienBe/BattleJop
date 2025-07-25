using BattleJop.Api.Application.Services.Matchs;
using BattleJop.Api.Application.Services.Rounds;
using BattleJop.Api.Web.Dtos;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BattleJop.Api.Web.Endpoints;

public class MatchModule : AbstractModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("matchs/{matchId:guid}/finish",
        [Tags("Matchs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [EndpointDescription("End of a match with the final score.")]
        async (Guid matchId, FinishMatchRequest request, IMatchService matchService, CancellationToken cancellationToken) =>
        {
            //Finish match
            var result = await matchService.FinishAsync(
                matchId,
                request.FirstTeamId,
                request.SecondTeamId,
                request.ScoreFirstTeam,
                request.ScoreSecondTeam,
                request.RemainingPuckFirstTeam,
                request.RemainingPuckSecondTeam,
                request.WinnerTeamId,
                cancellationToken);

            return ResolveActionResult(result);
        });
    }
}
