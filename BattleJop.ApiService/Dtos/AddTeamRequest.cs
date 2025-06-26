using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class AddTeamRequest
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("players")]
    public List<string> Players { get; set; } = [];
}
