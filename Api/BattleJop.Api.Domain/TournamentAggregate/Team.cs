﻿namespace BattleJop.Api.Domain.TournamentAggregate;

public class Team : Aggregate
{
    public Team()
    {
        
    }

    public string Name { get; private set; }

    public Tournament Tournament { get; private set; }

    public ICollection<Player> Players { get; private set; }

    public ICollection<MatchTeam> Scores { get; private set; }

    public Team(Guid id, string name, Tournament tournament)
    {
        ArgumentNullException.ThrowIfNull(tournament, nameof(tournament));

        Id = id;
        Name = name;
        Tournament = tournament;
        Players = [];
        Scores = [];
    }

    public void AddPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player, nameof(player));
        Players.Add(player);
    }
}
