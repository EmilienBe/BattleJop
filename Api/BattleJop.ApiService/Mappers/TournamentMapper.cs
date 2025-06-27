using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;

namespace BattleJop.Api.Web.Mappers;

public static class TournamentMapper
{
    public static TournamentResponse ToTournamentResponse(this Tournament source) => new TournamentResponse { Id =  source.Id , Name = source.Name, State = source.State};
}
