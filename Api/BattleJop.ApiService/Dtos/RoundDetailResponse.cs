using BattleJop.Api.Domain.TournamentAggregate;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web.Dtos
{
    public class RoundDetailResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("runningOrder")]
        public int RunningOrder { get; set; }

        [JsonPropertyName("state")]
        public RoundState State { get; set; }

        [JsonPropertyName("matchs")]
        public List<MatchResponse> Matchs { get; set; }

    }

    public class MatchResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("runningOrder")]
        public int RunningOrder { get; set; }

        [JsonPropertyName("state")]
        public MatchState State { get; set; }

        [JsonPropertyName("firstTeam")]
        public TeamScoreResponse FirstTeamScore { get; set; }

        [JsonPropertyName("secondTeam")]
        public TeamScoreResponse SecondTeamScore { get; set; }
    }

    public class TeamScoreResponse
    {
        [JsonPropertyName("teamId")]
        public Guid TeamId { get; set; }

        [JsonPropertyName("teamName")]
        public string TeamName { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("isWinner")]
        public bool IsWinner { get; set; }

        [JsonPropertyName("remainingPuck")]
        public int RemainingPuck { get; set; }
    }
}
