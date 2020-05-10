using System.Threading;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.Infrastructure.Data
{
    public class CodeHuntContext : DbContext, IUnitOfWork
    {
        public CodeHuntContext(DbContextOptions options) : base(options)
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
                .ToTable("GameResponse");

            modelBuilder.Entity<Sighting>()
                .ToTable("Sighting")
                .HasOne(x  => x.Team)
                .WithMany(x => x.Sightings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
                .ToTable("TeamResponse")
                .HasMany(x => x.CollectedItems)
                .WithOne(x => x.Team);

            modelBuilder.Entity<Team>()
                .HasMany(x => x.Sightings)
                .WithOne(x => x.Team);

            modelBuilder.Entity<Guard>()
                .ToTable("GuardResponse");

            modelBuilder.Entity<CollectableItem>()
                .ToTable("CollectableItemResponse");

            modelBuilder.Entity<CollectedItem>()
                .ToTable("CollectedItem");

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
