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
                    Email = "olof.svensson@epost.se",
                    FirstName = "Olof",
                    LastName = "Svensson",
                    PhoneNumber = "0723338282",
                    AddressId = 1,
                },
                new Customer
                {
                    Id = 2,
                    Email = "jan.jansson@epost.se",
                    FirstName = "Jan",
                    LastName = "Jansson",
                    PhoneNumber = "0723557781",
                    AddressId = 2,
                },
                new Customer
                {
                    Id = 3,
                    Email = "julia.smith@epost.se",
                    FirstName = "Julia",
                    LastName = "Smith",
                    PhoneNumber = "0723219981",
                    AddressId = 3,
                },
                new Customer
                {
                    Id = 4,
                    Email = "kajsa.fisk@epost.se",
                    FirstName = "Kajsa",
                    LastName = "Fisk",
                    PhoneNumber = "0733447785",
                    AddressId = 4,
                });

            builder.Entity<Address>()
                .HasData(new Address
                {
                    Id = 1,
                    BillingAddress = "Blåbärsstigen 99",
                    DeliveryAddress = "Solrosvägen 88",
                    PostalCode = "11322",
                    City = "Örebro",
                    Country = "Sverige",
                },
                new Address
                {
                    Id = 2,
                    BillingAddress = "Krickelinsväg 101",
                    DeliveryAddress = "Snöflingegatan 202",
                    PostalCode = "55667",
                    City = "Stockholm",
                    Country = "Sverige",
                },
                new Address
                {
                    Id = 3,
                    BillingAddress = "Månstrålevägen 45",
                    DeliveryAddress = "Regnbågsgatan 12",
                    PostalCode = "22433",
                    City = "Umeå",
                    Country = "Sverige",
                },
                new Address
                {
                    Id = 4,
                    BillingAddress = "Silverbäcksvägen 77",
                    DeliveryAddress = "Älvdalsvägen 34",
                    PostalCode = "77889",
                    City = "Paris",
                    Country = "Frankrike",
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
                   Comment = "En vit, rund mössa med svart skärm och en kokard framtill, traditionellt buren vid svenska studentexamina",
                   ImageUrl = "/assets/hats/Student.jpg",
                   IsDeleted = false,
                   Name = "Studenthatt",
                   Price = 1500,
                   Quantity = 2,
                   Size = 10,
                   Depth = 5,
                   Length = 12,
                   Width = 10,
                   HatTypeId = 1
               }, 
               new Hat
               {
                   Id = 2,
                   Comment = "En formell vit hatt med svart skärm och guldbroderad dekoration, som symboliserar sjöfartsbefäl.",
                   ImageUrl = "/assets/hats/Kaptenshatt.jpg",
                   IsDeleted = false,
                   Name = "Kaptenshatt",
                   Price = 1000,
                   Quantity = 3,
                   Size = 9,
                   Depth = 4,
                   Length = 9,
                   Width = 7,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 3,
                   Comment = "En röd, cylinderformad hatt utan brätten, ofta prydd med en tofs, traditionellt buren i delar av Mellanöstern och Nordafrika.",
                   ImageUrl = "/assets/hats/Fez.jpg",
                   IsDeleted = false,
                   Name = "Fez",
                   Price = 1700,
                   Quantity = 2,
                   Size = 8,
                   Depth = 7,
                   Length = 8,
                   Width = 7,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 4,
                   Comment = "En bredbrättad hatt av filt eller halm, designad för att skydda mot sol och regn på den amerikanska prärien.",
                   ImageUrl = "/assets/hats/Cowboyhatt.jpg",
                   IsDeleted = false,
                   Name = "Cowboyhatt",
                   Price = 2000,
                   Quantity = 1,
                   Size = 12,
                   Depth = 6,
                   Length = 14,
                   Width = 10,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 5,
                   Comment = "En lätt och luftig hatt flätad av halm, perfekt för att ge skugga och svalka under soliga sommardagar.",
                   ImageUrl = "/assets/hats/Halmhatt.jpg",
                   IsDeleted = false,
                   Name = "Halmhatt",
                   Price = 1300,
                   Quantity = 1,
                   Size = 11,
                   Depth = 5,
                   Length = 12,
                   Width = 9,
                   HatTypeId = 1
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
                    HatId = 5,
                    OrderId = 2,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 4,
                    HatId = 4,
                    OrderId = 2,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 5,
                    HatId = 2,
                    OrderId = 2,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 6,
                    HatId = 2,
                    OrderId = 3,
                    UserId = null
                }, 
                new OrderHat
                {
                    Id = 7,
                    HatId = 3,
                    OrderId = 4,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 8,
                    HatId = 5,
                    OrderId = 4,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 9,
                    HatId = 2,
                    OrderId = 5,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 10,
                    HatId = 4,
                    OrderId = 5,
                    UserId = null
                },
                new OrderHat
                {
                    Id = 11,
                    HatId = 1,
                    OrderId = 6,
                    UserId = null
                }
                );

            builder.Entity<Order>()
                .HasData(
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        OrderStatusId = 1,
                        StartDate = new DateTime(2025, 04, 01),
                        EndDate = new DateTime(2025, 11, 22),
                        Priority = false,
                        Price = 3125,
                        
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 2,
                        OrderStatusId = 2,
                        StartDate = new DateTime(2025, 04, 12),
                        EndDate = new DateTime(2025, 10, 14),
                        Priority = true,
                        Price = 6450
                        
                    },
                    new Order
                    {
                        Id = 3,
                        CustomerId = 2,
                        OrderStatusId = 2,
                        StartDate = new DateTime(2025, 03, 19),
                        EndDate = new DateTime(2025, 04, 30),
                        Priority = true,
                        Price = 600

                    },
                    new Order
                    {
                        Id = 4,
                        CustomerId = 3,
                        OrderStatusId = 3,
                        StartDate = new DateTime(2025, 04, 16),
                        EndDate = new DateTime(2025, 04, 21),
                        Priority = false,
                        Price = 600
                    },
                    new Order
                    {
                        Id = 5,
                        CustomerId = 4,
                        OrderStatusId = 1,
                        StartDate = new DateTime(2025, 04, 16),
                        EndDate = new DateTime(2025, 08, 29),
                        Priority = true,
                        Price = 600
                    },
                    new Order
                    {
                        Id = 6,
                        CustomerId = 4,
                        OrderStatusId = 2,
                        StartDate = new DateTime(2025, 04, 10),
                        EndDate = new DateTime(2025, 04, 30),
                        Priority = false,
                        Price = 600
                    }
                );
        }
    }
}
