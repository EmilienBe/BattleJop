using BattleJop.Api.Core;
using BattleJop.Api.Infrastructure.Datas;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace BattleJop.Api.Tests;

public class BattleJopWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _postgresSqlContainer;
    protected BattleJopDbContext _dbContext;


    public async Task InitializeAsync()
    {
        _postgresSqlContainer = new PostgreSqlBuilder().Build();
        await _postgresSqlContainer.StartAsync().ConfigureAwait(false);

        Environment.SetEnvironmentVariable(EnvironmentVariable.BATTLE_JOP_DB, _postgresSqlContainer.GetConnectionString());
        _dbContext = new BattleJopDbContext();

        await _dbContext.Database.MigrateAsync().ConfigureAwait(false);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgresSqlContainer.DisposeAsync().ConfigureAwait(false);
        await _dbContext.DisposeAsync().ConfigureAwait(false);
    }
}
