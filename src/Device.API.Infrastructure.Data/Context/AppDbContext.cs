using Device.API.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Device.API.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<DeviceEntity> Devices => Set<DeviceEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceEntity>(entity =>
            {
                entity.ToTable("device");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
                entity.Property(e => e.State).IsRequired();
                entity.Property(e => e.CreationTime).IsRequired();
            });
        }
    }

}
