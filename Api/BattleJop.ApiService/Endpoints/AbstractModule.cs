using BattleJop.Api.Core.ModelActionResult;

namespace BattleJop.Api.Web.Endpoints;

public abstract class AbstractModule
{
    protected IResult ResolveActionResult<T, TResult>(ModelActionResult<T> modelActionResult, TResult result, string createdUri = default!) where T : class
    {
        switch (modelActionResult.FaultType)
        {
            case FaultType.TOURNAMENT_NOT_FOUND:
            case FaultType.TEAM_NOT_FOUND:
                return Results.NotFound(new ErrorResponse(modelActionResult.FaultType, modelActionResult.Message));
            case FaultType.TOURNAMENT_INVALID_STATE:
            case FaultType.TOURNAMENT_INVALID_NUMBER_TEAMS:
            case FaultType.TOURNAMENT_NO_ROUND_EXIST:
                return Results.Conflict(new ErrorResponse(modelActionResult.FaultType, modelActionResult.Message));
            case FaultType.OK:
                return Results.Ok(result);
            case FaultType.CREATED:
                return Results.Created(createdUri, result);
            case FaultType.OK_NO_CONTENT:
                return Results.NoContent();
            default:
                throw new NotImplementedException();
        }
    }

    protected IResult ResolveActionResult(ModelActionResult modelActionResult)
    {
        switch (modelActionResult.FaultType)
        {
            case FaultType.TOURNAMENT_NOT_FOUND:
            case FaultType.TEAM_NOT_FOUND:
                return Results.NotFound(new ErrorResponse(modelActionResult.FaultType, modelActionResult.Message));
            case FaultType.TOURNAMENT_INVALID_STATE:
            case FaultType.TOURNAMENT_INVALID_NUMBER_TEAMS:
            case FaultType.TOURNAMENT_NO_ROUND_EXIST:
                return Results.Conflict(new ErrorResponse(modelActionResult.FaultType, modelActionResult.Message));
            case FaultType.OK_NO_CONTENT:
                return Results.NoContent();
            default:
                throw new NotImplementedException();
        }
    }
}