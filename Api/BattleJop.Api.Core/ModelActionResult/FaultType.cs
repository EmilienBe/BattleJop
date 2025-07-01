namespace BattleJop.Api.Core.ModelActionResult;

public enum FaultType
{
    TOURNAMENT_NOT_FOUND = 10001,
    TEAM_NOT_FOUND = 10002,
    OK = 10003,
    CREATED = 10004,
    OK_NO_CONTENT = 10005,
    TOURNAMENT_IS_IN_PROGRESS_OR_FINISHED = 10006
}
