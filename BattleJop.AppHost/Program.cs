var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BattleJop_Api_Web>("BattleJop-Api-Web");

builder.AddProject<Projects.BattleJop_Web>("BattleJop-App-Web")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
