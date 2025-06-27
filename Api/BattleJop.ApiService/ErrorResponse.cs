using BattleJop.Api.Core.ModelActionResult;
using System.Text.Json.Serialization;

namespace BattleJop.Api.Web;

public class ErrorResponse
{
    [JsonPropertyName("date")]
    public long Date { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    public ErrorResponse()
    {
        
    }

    public ErrorResponse(FaultType fault, string message)
    {
        Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Code = (int) fault;
        Error = fault.ToString();
        Message = message;
    }
}
