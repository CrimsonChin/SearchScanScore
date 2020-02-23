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
        public DbSet<CollectableItem> CollectableItems { get; set; }
        public DbSet<CollectedItem> CollectedItems { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .ToTable("Game");

            modelBuilder.Entity<Team>()
                .ToTable("Team")
                .HasMany(x => x.CollectedItems)
                .WithOne(x => x.Team);

            modelBuilder.Entity<CollectableItem>()
                .ToTable("CollectableItem");

            modelBuilder.Entity<CollectedItem>()
                .ToTable("CollectedItem");
        }
    }
}
