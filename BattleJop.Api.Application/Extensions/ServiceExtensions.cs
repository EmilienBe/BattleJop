using BattleJop.Api.Application.Services.Tournaments;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BattleJop.Api.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITournamentService, TournamentService>();

        return services;
    }
}
