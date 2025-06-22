using BattleJop.Api.Infrastructure.Datas;
using BattleJop.Api.Infrastructure.Repositories;
using BattleJop.Api.Infrastructure.Repositories.Tournament;
using Microsoft.Extensions.DependencyInjection;

namespace BattleJop.Api.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<BattleJopCommandDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();

        return services;
    }
}
