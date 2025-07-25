using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class RankingResponse
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("teamId")]
    public Guid TeamId { get; set; }

    [JsonPropertyName("teamName")]
    public string TeamName { get; set; } = default!;

    [JsonPropertyName("numberOfVictory")]
    public int NumberOfVictory { get; set; }

    [JsonPropertyName("numberOfDefeat")]
    public int NumberOfDefeat { get; set; }

    [JsonPropertyName("totalScore")]
    public int TotalScore { get; set; }

    [JsonPropertyName("totalRemainingPuck")]
    public int TotalRemainingPuck { get; set; }
}
