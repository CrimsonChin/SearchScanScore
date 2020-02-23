using Data.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Data
{
    public class SearchScanScoreContext : DbContext
    {
        public SearchScanScoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<CollectableItem> CollectableItems { get; set; }
        public DbSet<CollectedItem> CollectedItems { get; set; }    
        public DbSet<Sighting> Sightings { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .ToTable("Game");

            modelBuilder.Entity<Sighting>()
                .ToTable("Sighting")
                .HasOne(x  => x.Team)
                .WithMany(x => x.Sightings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
                .ToTable("Team")
                .HasMany(x => x.CollectedItems)
                .WithOne(x => x.Team);

            modelBuilder.Entity<Team>()
                .HasMany(x => x.Sightings)
                .WithOne(x => x.Team);

            modelBuilder.Entity<Guard>()
                .ToTable("Guard");

            modelBuilder.Entity<CollectableItem>()
                .ToTable("CollectableItem");

            modelBuilder.Entity<CollectedItem>()
                .ToTable("CollectedItem");

        }
    }
}
