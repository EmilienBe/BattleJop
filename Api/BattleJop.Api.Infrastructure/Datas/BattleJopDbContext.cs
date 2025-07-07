using BattleJop.Api.Core;
using BattleJop.Api.Domain;
using BattleJop.Api.Domain.TournamentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BattleJop.Api.Infrastructure.Datas;

public class BattleJopDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Match
        modelBuilder.Entity<Match>()
            .ToTable("matchs")
            .HasKey(x => x.Id);

        modelBuilder.Entity<Match>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Player
        modelBuilder.Entity<Player>()
            .ToTable("players")
            .HasKey(x => x.Id);

        modelBuilder.Entity<Player>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Round
        modelBuilder.Entity<Round>()
            .ToTable("rounds")
            .HasKey(x => x.Id);

        modelBuilder.Entity<Round>()
            .HasMany(x => x.Matchs)
            .WithOne(x => x.Round)
            .HasForeignKey("RoundId");

        modelBuilder.Entity<Round>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Team
        modelBuilder.Entity<Team>()
            .ToTable("teams")
            .HasKey(x => x.Id);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey("TeamId");

        //modelBuilder.Entity<Team>()
            //.HasMany(t => t.Scores)
            //.WithMany(m => m.Teams)
            //.UsingEntity<MatchTeam>(
            //    r => r.HasOne<Match>().WithMany().HasForeignKey(e => e.MatchId),
            //    l => l.HasOne<Team>().WithMany().HasForeignKey(e => e.TeamId)
            //);

        modelBuilder.Entity<Team>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Tournament
        modelBuilder.Entity<Tournament>()
            .ToTable("tournaments")
            .HasKey(x => x.Id);

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Teams)
            .WithOne(t => t.Tournament)
            .HasForeignKey("TournamentId");

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Rounds)
            .WithOne(t => t.Tournament)
            .HasForeignKey("TournamentId");

        modelBuilder.Entity<Tournament>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");

        //Match Teams

        modelBuilder.Entity<MatchTeam>()
            .ToTable("match_teams")
            .HasKey(x => x.Id); 

        modelBuilder.Entity<MatchTeam>()
            .HasOne(s => s.Match).WithMany(m => m.Scores);

        modelBuilder.Entity<MatchTeam>()
            .HasOne(s => s.Team).WithMany(t => t.Scores);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connexionString = Environment.GetEnvironmentVariable(EnvironmentVariable.BATTLE_JOP_DB);
        optionsBuilder
            .UseNpgsql(connexionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }

    public override int SaveChanges()
    {
        UpdateDateFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateDateFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateDateFields()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Aggregate && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries.Where(e => e.State == EntityState.Modified))
            ((Aggregate)entry.Entity).LastUpdated = DateTime.UtcNow;


        foreach (var entry in entries.Where(e => e.State == EntityState.Added))
            ((Aggregate)entry.Entity).Created = DateTime.UtcNow;
    }

    public DbSet<Match> Matchs { get; set; }
    public DbSet<MatchTeam> MatchTeams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
}
