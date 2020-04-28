using RoskhTest.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RoskhTest.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<PackageState> PackageStates { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageItem> PackageItems { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PackageState>(entity =>
            {
                entity.HasData(
                    new PackageState()
                    {
                        Id = (int)DeliveryState.WfPU,
                        Code = DeliveryState.WfPU.ToString(),
                        Name = "Waiting for Pick Up",
                        Description = "Csomag a feladónál. Futárra vár."
                    },
                    new PackageState()
                    {
                        Id = (int)DeliveryState.PU,
                        Code = DeliveryState.PU.ToString(),
                        Name = "Picked Up",
                        Description = "Csomag a futárnál. Depóba tart."
                    },
                    new PackageState()
                    {
                        Id = (int)DeliveryState.ID,
                        Code = DeliveryState.ID.ToString(),
                        Name = "In Depo",
                        Description = "Depóban van. Kiszállításra vár."
                    },
                    new PackageState()
                    {
                        Id = (int)DeliveryState.OD,
                        Code = DeliveryState.OD.ToString(),
                        Name = "On Delivery",
                        Description = "Kiszállítás alatt. Célba tart."
                    },
                    new PackageState()
                    {
                        Id = (int)DeliveryState.DD,
                        Code = DeliveryState.DD.ToString(),
                        Name = "Delivered",
                        Description = "Kiszállítva."
                    }
                );
            });
            
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PackageState>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(p => p.State)
                    .WithMany(ps => ps.Packages)
                    .HasForeignKey(p => p.StateId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                entity.HasOne(p => p.Owner)
                    .WithMany(u => u.Packages)
                    .HasForeignKey(p => p.OwnerId)
                    .OnDelete(DeleteBehavior.SetNull);

            });

            modelBuilder.Entity<PackageItem>(entity =>
            {
                entity.HasKey(e => new {e.PackageId, e.ItemId});
                entity.HasOne(e => e.Package)
                    .WithMany(e => e.PackageItems)
                    .HasForeignKey(e => e.PackageId);
                entity.HasOne(e => e.Item)
                    .WithMany(e => e.PackageItems)
                    .HasForeignKey(e => e.ItemId);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}