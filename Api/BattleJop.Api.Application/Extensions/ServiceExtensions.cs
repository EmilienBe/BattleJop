using BattleJop.Api.Application.Services.Matchs;
using BattleJop.Api.Application.Services.Rounds;
using BattleJop.Api.Application.Services.Teams;
using BattleJop.Api.Application.Services.Tournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BattleJop.Api.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IRoundService, RoundService>();
        services.AddScoped<IMatchService, MatchService>();

        return services;
    }
}
