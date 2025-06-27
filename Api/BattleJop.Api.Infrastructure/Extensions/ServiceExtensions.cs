using BattleJop.Api.Infrastructure.Datas;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Infrastructure.Repositories.Players;
using BattleJop.Api.Infrastructure.Repositories.Teams;
using BattleJop.Api.Infrastructure.Repositories.Tournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BattleJop.Api.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<BattleJopDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITournamentCommandRepository, TournamentCommandRepository>();
        services.AddScoped<ITournamentQueryRepository, TournamentQueryRepository>();

        services.AddScoped<ITeamCommandRepository, TeamCommandRepository>();
        services.AddScoped<ITeamQueryRepository, TeamQueryRepository>();

        services.AddScoped<IPlayerCommandRepository, PlayerCommandRepository>();
        services.AddScoped<IPlayerQueryRepository, PLayerQueryRepository>();

        return services;
    }
}
