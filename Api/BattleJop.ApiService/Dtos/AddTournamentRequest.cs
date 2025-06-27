using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class AddTournamentRequest
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("numberRounds")]
    public int NumberRounds { get; set; }
}
