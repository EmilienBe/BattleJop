using BattleJop.Api.Domain.TournamentAggregate;
using BattleJop.Api.Web.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BattleJop.Api.Tests.Web.Endpoints.Tournaments;

public class AddTournamentTest (BattleJopWebAppFactory factory) : AbstractIntegrationTest<BattleJopWebAppFactory> (factory)
{
    [Fact]
    public async Task AddTournament_ShouldReturn201_WhenTournamentIsCreated()
    {
        //Arrange
        var name = "Add Tournament test";
        var numberOfRounds = 5;

        //Act
        var response = await _client.PostAsJsonAsync("tournaments", new AddTournamentRequest 
        { 
            Name = name,
            NumberRounds = numberOfRounds
        });

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode );

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<TournamentResponse>(json);

        Assert.NotNull( data );
        Assert.Equal( name, data.Name );
        Assert.Equal(TournamentState.InConfiguration, data.State);

        ClearDatabase();
    }

    [Theory]
    [InlineData("",1)]
    [InlineData("Tournament name",0)]
    public async Task AddTournament_ShouldReturn400_WhenInputIsInvalid(string name, int numberOfRounds)
    {
        //Act
        var response = await _client.PostAsJsonAsync("tournaments", new AddTournamentRequest
        {
            Name = name,
            NumberRounds = numberOfRounds
        });

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
