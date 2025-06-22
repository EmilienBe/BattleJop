using BattleJop.Api.Domain;
using BattleJop.Api.Domain.TournamentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BattleJop.Api.Infrastructure.Datas;

public class BattleJopCommandDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Match
        modelBuilder.Entity<Match>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Match>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Player
        modelBuilder.Entity<Player>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Player>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Round
        modelBuilder.Entity<Round>()
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
            .HasKey(x => x.Id);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey("TeamId");

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Matchs)
            .WithMany(m => m.Teams)
            .UsingEntity<MatchTeam>(
                r => r.HasOne<Match>().WithMany().HasForeignKey(e => e.MatchId),
                l => l.HasOne<Team>().WithMany().HasForeignKey(e => e.TeamId)
            );

        modelBuilder.Entity<Team>()
            .Property(b => b.Desactivated)
            .HasDefaultValueSql("false");


        //Tournament
        modelBuilder.Entity<Tournament>()
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

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //var connexionString = Environment.GetEnvironmentVariable(EnvironmentVariable.BATTLE_JOP_DB);
        var connexionString = "User ID=postgres;Password=azerty;Host=localhost;Port=5432;Database=BattleJopDb;";
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
    public DbSet<Player> Players { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
}
