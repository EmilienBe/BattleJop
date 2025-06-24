using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos.AddTournament;

public class AddTournamentResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

}
