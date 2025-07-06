using BattleJop.Api.Core.ModelActionResult;
using System.Collections.Generic;

namespace BattleJop.Api.Domain.TournamentAggregate;

public class Tournament : Aggregate
{
    public Tournament()
    {

    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public TournamentState State { get; private set; }

    public ICollection<Round> Rounds { get; private set; } = [];

    public ICollection<Team> Teams { get; private set; } = [];

    public Tournament(Guid id, string name)
    {
        Id = id;
        Name = name;
        State = TournamentState.InConfiguration;
        Rounds = [];
        Teams = [];
    }

    public void AddRound(Round round)
    {
        ArgumentNullException.ThrowIfNull(round);
        Rounds.Add(round);
    }

    public void AddTeam(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);

        Teams.Add(team);
    }

    public void UpdateName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    public void UpdateState(TournamentState state) => State = state;

    public bool IsInConfiguration() => State == TournamentState.InConfiguration;

    public void Finish() => State = TournamentState.Finished;

    public (List<Match> matches, List<MatchTeam> matchTeams) GenerateInitialMatches(Round round)
    {
        var teams = ShuffleTeams();

        int matchOrder = 1;

        var matchs = new List<Match>();
        var matchTeams = new List<MatchTeam>();

        for (int i = 0; i < teams.Count; i += 2)
        {
            var match = new Match(Guid.NewGuid(), matchOrder++, round);

            var firstMatchTeam = new MatchTeam(Guid.NewGuid(), match, teams[i]);
            var secondMatchTeam = new MatchTeam(Guid.NewGuid(), match, teams[i + 1]);

            matchs.Add(match);
            matchTeams.Add(firstMatchTeam);
            matchTeams.Add(secondMatchTeam);
        }

        return (matchs, matchTeams);
    }

    private List<Team> ShuffleTeams()
    {
        var random = new Random();

        var teams = Teams.ToList();

        int oldIndex = Teams.Count;
        while (oldIndex > 1)
        {
            oldIndex--;
            int newIndex = random.Next(oldIndex + 1);
            var team = teams[newIndex];
            teams[newIndex] = teams[oldIndex];
            teams[oldIndex] = team;
        }
        return teams;
    }
}

public enum TournamentState
{
    InConfiguration = 0,
    InProgress = 1,
    Finished = 2
}