using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;

namespace BattleJop.Api.Web.Mappers;

public static class TeamMapper
{
    public static TeamResponse ToTeamResponse(this Team source) => new TeamResponse { Id = source.Id, Name = source.Name, Players = [.. source.Players.Select(p => p.ToPlayerResponse())] };
    public static PlayerResponse ToPlayerResponse(this Player source) => new PlayerResponse { Id = source.Id, Name = source.Name };
}
