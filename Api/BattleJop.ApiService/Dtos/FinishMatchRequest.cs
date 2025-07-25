using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class FinishMatchRequest
{
    [JsonRequired]
    [JsonPropertyName("firstTeamId")]
    public Guid FirstTeamId { get; set; }

    [JsonRequired]
    [JsonPropertyName("secondTeamId")]
    public Guid SecondTeamId { get; set; }

    [JsonRequired]
    [JsonPropertyName("winnerTeamId")]
    public Guid WinnerTeamId { get; set; }

    [JsonRequired]
    [JsonPropertyName("scoreFirstTeam")]
    public int ScoreFirstTeam { get; set; }

    [JsonRequired]
    [JsonPropertyName("scoreSecondTeam")]
    public int ScoreSecondTeam { get; set; }

    [JsonRequired]
    [JsonPropertyName("remainingPuckFirstTeam")]
    public int RemainingPuckFirstTeam { get; set; }

    [JsonRequired]
    [JsonPropertyName("remainingPuckSecondTeam")]
    public int RemainingPuckSecondTeam { get; set; }
} 