using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BattleJop.Api.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(option => option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
