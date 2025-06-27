using BattleJop.Api.Infrastructure.Datas;
using BattleJop.Api.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BattleJop.Api.Tests
{
    [Collection("BattleJop.Api")]
    public abstract class AbstractIntegrationTest<TFactory> where TFactory : BattleJopWebAppFactory
    {
        protected readonly HttpClient _client;
        protected BattleJopDbContext _context;

        protected AbstractIntegrationTest(TFactory factory)
        {
            var scope = factory.Services.CreateScope();
            _client = factory.CreateClient();
            _context = scope.ServiceProvider.GetRequiredService<BattleJopDbContext>();
        }

        protected void ClearDatabase() =>
            _context.Database.ExecuteSqlRaw("TRUNCATE match_teams, players, matchs, teams, rounds, tournaments;");
    }
}

[CollectionDefinition("BattleJop.Api")]
public class BattleJopWebAppFactoryCollection : ICollectionFixture<BattleJopWebAppFactory>
{

}