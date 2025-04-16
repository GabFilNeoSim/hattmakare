using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Hattmakare.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hat> Hats { get; set; }
        public DbSet<StandardHat> StandardHats { get; set; }
        public DbSet<SpecialHat> SpecialHats { get; set; }
        public DbSet<HatMaterial> HatsMaterial { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHat> OrderHats { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasOne(x => x.Address)
                .WithMany(x => x.Customers)
                .OnDelete(DeleteBehavior.NoAction);

            // Hat inheritance
            builder.Entity<Hat>().ToTable("Hats"); 
            builder.Entity<StandardHat>().ToTable("StandardHats");
            builder.Entity<SpecialHat>().ToTable("SpecialHats");

            // Hat material
            builder.Entity<HatMaterial>()
                .HasKey(x => new { x.HatId, x.MaterialId });

            builder.Entity<HatMaterial>()
                .HasOne(x => x.Hat)
                .WithMany(x => x.HatMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<HatMaterial>()
                .HasOne(x => x.Material)
                .WithMany(x => x.HatMaterials)
                .OnDelete(DeleteBehavior.NoAction);

            // Order hat
            builder.Entity<OrderHat>()
                .HasKey(x => new { x.HatId, x.OrderId, x.UserId });

            builder.Entity<OrderHat>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderHat>()
                .HasOne(x => x.Hat)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderHat>()
                .HasOne(x => x.User)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderStatus>()
                .HasData(new OrderStatus
                {
                    Id = 1,
                    Name = "Startad"
                });
            builder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        OrderStatusId = 1,
                        StartDate = new DateOnly(2025, 04, 16),
                        EndDate = new DateOnly(2025, 04, 17),
                        Priority = false,
                        Price = 500
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 1,
                        OrderStatusId = 1,
                        StartDate = new DateOnly(2025, 04, 16),
                        EndDate = new DateOnly(2025, 04, 17),
                        Priority = true,
                        Price = 600
                    }
                );
        }
    }
}
