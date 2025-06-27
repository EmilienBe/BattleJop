using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos
{
    public class TeamResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("players")]
        public List<PlayerResponse> Players { get; set; } = [];
    }

    public class PlayerResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
