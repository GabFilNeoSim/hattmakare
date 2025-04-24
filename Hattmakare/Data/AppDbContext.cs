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
        public DbSet<HatMaterial> HatsMaterial { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHat> OrderHats { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HatType> HatTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasOne(x => x.Address)
                .WithMany(x => x.Customers)
                .OnDelete(DeleteBehavior.NoAction);

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
            //builder.Entity<OrderHat>()
            //    .HasKey(x => new { x.HatId, x.OrderId, x.UserId });

            builder.Entity<OrderHat>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderHat>()
                .HasOne(x => x.Hat)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderHat>()
                .HasOne(x => x.User)
                .WithMany(x => x.OrderHats)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>()
                .HasData(new Customer
                {
                    Id = 1,
                    Email = "testmejl",
                    FirstName = "Olof",
                    LastName = "Svensson",
                    PhoneNumber = "1234567890",
                    AddressId = 1,
                },
                new Customer
                {
                    Id = 2,
                    Email = "testmejl",
                    FirstName = "Jan",
                    LastName = "Jansson",
                    PhoneNumber = "1234567890",
                    AddressId = 1,
                }
            );

            builder.Entity<Address>()
                .HasData(new Address
                {
                    Id = 1,
                    BillingAddress = "Adress 1",
                    DeliveryAddress = "Adress 2",
                    PostalCode = "12345",
                    City = "Örebro",
                    Country = "Sverige",
                     

                });

            builder.Entity<OrderStatus>()
                .HasData(new OrderStatus
                {
                    Id = 1,
                    Name = "Ej påbörjad"
                });
            builder.Entity<OrderStatus>()
                .HasData(new OrderStatus
                {
                    Id = 2,
                    Name = "Påbörjad"
                });
            builder.Entity<OrderStatus>()
            .HasData(new OrderStatus
                {
                    Id = 3,
                    Name = "Klar"
                });
            builder.Entity<HatType>()
                .HasData(new HatType
                {
                    Id = 1,
                    Name = "Standardhatt"

                }, new HatType
                {
                    Id = 2,
                    Name = "Standardhatt med tillägg"
                }, new HatType
                {
                    Id = 3, 
                    Name = "Specialhatt"

                });


            builder.Entity<Hat>()
               .HasData(new Hat
               {
                   Id = 1,
                   Comment = "Testcomment",
                   ImageUrl = null,
                   IsDeleted = false,
                   Name = "Studenthatt",
                   Price = 5,
                   Quantity = 2,
                   Size = 10,
                   HatTypeId = 1
               }, new Hat
               {
                   Id = 2,
                   Comment = "Testcomment",
                   ImageUrl = null,
                   IsDeleted = false,
                   Name = "Kaptenshatt",
                   Price = 52,
                   Quantity = 5,
                   Size = 8
               });

            
            builder.Entity<OrderHat>()
                .HasData(new OrderHat
                {
                    Id = 1,
                    HatId = 1,
                    OrderId = 1,
                    UserId = null,
                }, new OrderHat
                {
                    Id = 2,
                    HatId = 2,
                    OrderId = 1,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 3,
                    HatId = 2,
                    OrderId = 2,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 4,
                    HatId = 2,
                    OrderId = 3,
                    UserId = null
                }
                );

            //builder.Entity<HatMaterial>()
            //    .HasData(
            //        new HatMaterial
            //        {
            //            HatId = 1,
            //            MaterialId = 1,
            //            Quantity = 5
            //        },
            //        new HatMaterial
            //        {
            //            HatId = 1,
            //            MaterialId = 2,
            //            Quantity = 1
            //        },
            //        new HatMaterial
            //        {
            //            HatId = 2,
            //            MaterialId = 4,
            //            Quantity = 54
            //        },
            //        new HatMaterial
            //        {
            //            HatId = 2,
            //            MaterialId = 5,
            //            Quantity = 5
            //        }
            //    );

            builder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        OrderStatusId = 1,
                        StartDate = new DateTime(2025, 04, 16),
                        EndDate = new DateTime(2025, 04, 17),
                        Priority = false,
                        Price = 500,
                        
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 2,
                        OrderStatusId = 2,
                        StartDate = new DateTime(2025, 04, 16),
                        EndDate = new DateTime(2025, 04, 17),
                        Priority = true,
                        Price = 600
                        
                    },
                    new Order
                    {
                        Id = 3,
                        CustomerId = 2,
                        OrderStatusId = 2,
                        StartDate = new DateTime(2025, 04, 16),
                        EndDate = new DateTime(2025, 04, 17),
                        Priority = true,
                        Price = 600

                    }
                );
        }
    }
}
