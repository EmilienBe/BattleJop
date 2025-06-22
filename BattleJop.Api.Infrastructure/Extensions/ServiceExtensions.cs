using BattleJop.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BattleJop.Api.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<UnitOfWork>();

        return services;
    }
}
