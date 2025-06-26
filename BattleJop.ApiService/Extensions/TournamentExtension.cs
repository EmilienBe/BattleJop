using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos.AddTournament;

namespace BattleJop.Api.Web.Extensions;

public static class TournamentExtension
{
    public static AddTournamentResponse ToAddTournamentresponse(this Tournament source) => new AddTournamentResponse { Id =  source.Id , Name = source.Name, State = source.State};
}
