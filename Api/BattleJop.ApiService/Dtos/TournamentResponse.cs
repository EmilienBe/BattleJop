using BattleJop.Api.Domain.TournamentAggregate;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos;

public class TournamentResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter<TournamentState>))]
    public TournamentState State { get; set; }

}
