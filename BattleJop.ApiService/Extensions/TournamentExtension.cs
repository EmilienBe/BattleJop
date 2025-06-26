using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;

namespace BattleJop.Api.Web.Extensions;

public static class TournamentExtension
{
    public static TournamentResponse ToTournamentResponse(this Tournament source) => new TournamentResponse { Id =  source.Id , Name = source.Name, State = source.State};
}
