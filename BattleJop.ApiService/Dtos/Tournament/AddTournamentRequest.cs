using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos.Tournament;

public class AddTournamentRequest
{
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [Required]
    [JsonPropertyName("numberRounds")]
    public int NumberRounds { get; set; }
}
