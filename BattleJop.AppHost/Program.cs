
var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BattleJop_Api_Web>("BattleJop-Api-Web")
    .WithEnvironment("BATTLE_JOP_DB", "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=BattleJopDb;");

builder.AddProject<Projects.BattleJop_Web>("BattleJop-App-Web")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
