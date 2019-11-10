using aepetria_vue_api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aepetria_vue_api.DAL
{
    public class AepetriaDbContext : DbContext
    {
        public DbSet<RemoteImage> RemoteImages { get; set; }

        public AepetriaDbContext(DbContextOptions<AepetriaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RemoteImage>()
                .Property(x => x.IsActive)
                .HasDefaultValue(false);
            modelBuilder.Entity<RemoteImage>()
                .Property(x => x.Data)
                .HasColumnType("mediumblob");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var newEntities = ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                    x.Entity != null
                ).ToList();

            newEntities.ForEach(x => x.Property("Created").CurrentValue = DateTime.Now);

            var updatedEntities = ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Modified &&
                    x.Entity != null
                ).ToList();

            updatedEntities.ForEach(x => x.Property("LastModified").CurrentValue = DateTime.Now);

            return base.SaveChangesAsync();
        }
    }
}
