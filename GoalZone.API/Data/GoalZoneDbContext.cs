using GoalZone.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Data
{
    public class GoalZoneDbContext : DbContext
    {
        public GoalZoneDbContext(DbContextOptions<GoalZoneDbContext> options) : base(options) { }

        public DbSet<City> Cities => Set<City>();
        public DbSet<Season> Seasons => Set<Season>();
        public DbSet<Referee> Referees => Set<Referee>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<MatchEvent> MatchEvents => Set<MatchEvent>();
        public DbSet<MatchStatistic> MatchStatistics => Set<MatchStatistic>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Country).HasMaxLength(100);
            });

            modelBuilder.Entity<Season>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired().HasMaxLength(20);
                e.HasIndex(s => s.Name).IsUnique();
            });

            modelBuilder.Entity<Referee>(e =>
            {
                e.HasKey(r => r.Id);
                e.Property(r => r.FullName).IsRequired().HasMaxLength(100);
                e.Property(r => r.Nationality).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Username).IsRequired().HasMaxLength(50);
                e.Property(u => u.Email).IsRequired().HasMaxLength(200);
                e.Property(u => u.PasswordHash).IsRequired();
                e.Property(u => u.Role).HasMaxLength(20);
                e.HasIndex(u => u.Username).IsUnique();
                e.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Team>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Name).IsRequired().HasMaxLength(100);
                e.Property(t => t.ShortCode).IsRequired().HasMaxLength(5);
                e.HasIndex(t => t.ShortCode).IsUnique();
                e.Property(t => t.IsActive).HasDefaultValue(true);
                e.HasOne(t => t.City)
                 .WithMany(c => c.Teams)
                 .HasForeignKey(t => t.CityId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Match>(e =>
            {
                e.HasKey(m => m.Id);
                e.Property(m => m.Status).HasConversion<int>();
                e.HasOne(m => m.Season)
                 .WithMany(s => s.Matches)
                 .HasForeignKey(m => m.SeasonId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(m => m.HomeTeam)
                 .WithMany(t => t.HomeMatches)
                 .HasForeignKey(m => m.HomeTeamId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(m => m.AwayTeam)
                 .WithMany(t => t.AwayMatches)
                 .HasForeignKey(m => m.AwayTeamId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(m => m.Referee)
                 .WithMany(r => r.Matches)
                 .HasForeignKey(m => m.RefereeId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MatchEvent>(e =>
            {
                e.HasKey(ev => ev.Id);
                e.Property(ev => ev.EventType).HasConversion<int>();
                e.Property(ev => ev.Description).HasMaxLength(500);
                e.Property(ev => ev.PlayerName).HasMaxLength(100);
                e.Property(ev => ev.SubstitutedPlayerName).HasMaxLength(100);
                e.HasOne(ev => ev.Match)
                 .WithMany(m => m.MatchEvents)
                 .HasForeignKey(ev => ev.MatchId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MatchStatistic>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.HomePossession).HasColumnType("decimal(5,2)");
                e.Property(s => s.AwayPossession).HasColumnType("decimal(5,2)");
                e.Property(s => s.HomePassAccuracy).HasColumnType("decimal(5,2)");
                e.Property(s => s.AwayPassAccuracy).HasColumnType("decimal(5,2)");
                e.HasOne(s => s.Match)
                 .WithOne(m => m.MatchStatistic)
                 .HasForeignKey<MatchStatistic>(s => s.MatchId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
