using BattleJop.Api.Domain.TournamentAggregate;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class RoundResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("runningOrder")]
    public int RunningOrder { get; set; }

    [JsonPropertyName("state")]
    public RoundState State { get; set; }
}
