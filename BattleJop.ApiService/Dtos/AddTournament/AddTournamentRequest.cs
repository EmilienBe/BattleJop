using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos.AddTournament;

public class AddTournamentRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("numberRounds")]
    public int NumberRounds { get; set; }
}
