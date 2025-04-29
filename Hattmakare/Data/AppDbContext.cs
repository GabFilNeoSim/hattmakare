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

            builder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Ullfilt", Unit = "m", Price = 124.50m, Supplier = "Nordic Felt AB", IsDecoration = false },
                new Material { Id = 2, Name = "Kaninfilt", Unit = "m", Price = 499.90m, Supplier = "Hatter’s Choice Ltd", IsDecoration = false },
                new Material { Id = 3, Name = "Toquillastrå", Unit = "kg", Price = 419.75m, Supplier = "Ecuador Weaves Co.", IsDecoration = false },
                new Material { Id = 4, Name = "Rishalm", Unit = "kg", Price = 109.20m, Supplier = "StrawCraft Asia", IsDecoration = false },
                new Material { Id = 5, Name = "Palmlöv", Unit = "kg", Price = 94.60m, Supplier = "Tropic Naturals", IsDecoration = false },
                new Material { Id = 6, Name = "Majsblad", Unit = "kg", Price = 134.95m, Supplier = "EcoFiber Mexico", IsDecoration = false },
                new Material { Id = 7, Name = "Hampfibrer", Unit = "kg", Price = 159.50m, Supplier = "GreenTextiles GmbH", IsDecoration = false },
                new Material { Id = 8, Name = "Bomull", Unit = "m", Price = 59.90m, Supplier = "CottonLine Textiles", IsDecoration = false },
                new Material { Id = 9, Name = "Linne", Unit = "m", Price = 84.75m, Supplier = "NordTextil AB", IsDecoration = false },
                new Material { Id = 10, Name = "Ull", Unit = "m", Price = 109.00m, Supplier = "NordTextil AB", IsDecoration = false },
                new Material { Id = 11, Name = "Silke", Unit = "m", Price = 399.90m, Supplier = "Silken Touch Co.", IsDecoration = false },
                new Material { Id = 12, Name = "Satin", Unit = "m", Price = 129.50m, Supplier = "Fabric Elegance", IsDecoration = false },
                new Material { Id = 13, Name = "Tweed", Unit = "m", Price = 179.90m, Supplier = "Highland Textiles", IsDecoration = false },
                new Material { Id = 14, Name = "Polyester", Unit = "m", Price = 49.95m, Supplier = "GlobalPoly Ltd", IsDecoration = false },
                new Material { Id = 15, Name = "Läder", Unit = "st", Price = 849.00m, Supplier = "Scandi Leatherworks", IsDecoration = false },
                new Material { Id = 16, Name = "Lackerat papper", Unit = "st", Price = 0.75m, Supplier = "CraftMaterials.se", IsDecoration = false },
                new Material { Id = 17, Name = "Fuskpäls", Unit = "m", Price = 144.60m, Supplier = "FauxFur Fabrics", IsDecoration = false },
                new Material { Id = 18, Name = "Strutsfjäder", Unit = "st", Price = 35.00m, Supplier = "Bendigo Farm Inc", IsDecoration = true },
                new Material { Id = 19, Name = "Påfågelfjäder", Unit = "st", Price = 64.90m, Supplier = "FeatherWorks Ltd", IsDecoration = true },
                new Material { Id = 20, Name = "Hönsfjäder", Unit = "st", Price = 0.90m, Supplier = "FeatherWorks Ltd", IsDecoration = true },
                new Material { Id = 21, Name = "Tygblommor", Unit = "st", Price = 29.95m, Supplier = "DecoFlora AB", IsDecoration = true },
                new Material { Id = 22, Name = "Pärlor", Unit = "g", Price = 11.50m, Supplier = "Pearl Paradise", IsDecoration = true },
                new Material { Id = 23, Name = "Spets", Unit = "m", Price = 39.90m, Supplier = "Lace & Grace", IsDecoration = true },
                new Material { Id = 24, Name = "Lurextråd", Unit = "m", Price = 59.00m, Supplier = "ShinyThreads Co.", IsDecoration = true }
            );


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
                   ImageUrl = "Student.jpg",
                   IsDeleted = false,
                   Name = "Studenthatt",
                   Price = 1500,
                   Quantity = 2,
                   Size = 10,
                   Depth = 5,
                   Length = 23,
                   Width = 20,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 2,
                   Comment = "En formell vit hatt med svart skärm och guldbroderad dekoration, som symboliserar sjöfartsbefäl.",
                   ImageUrl = "Kaptenshatt.jpg",
                   IsDeleted = false,
                   Name = "Kaptenshatt",
                   Price = 1000,
                   Quantity = 3,
                   Size = 9,
                   Depth = 4,
                   Length = 22,
                   Width = 18,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 3,
                   Comment = "En röd, cylinderformad hatt utan brätten, ofta prydd med en tofs, traditionellt buren i delar av Mellanöstern och Nordafrika.",
                   ImageUrl = "Fez.jpg",
                   IsDeleted = false,
                   Name = "Fez",
                   Price = 800,
                   Quantity = 2,
                   Size = 8,
                   Depth = 9,
                   Length = 16,
                   Width = 16,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 4,
                   Comment = "En bredbrättad hatt av filt eller halm, designad för att skydda mot sol och regn på den amerikanska prärien.",
                   ImageUrl = "Cowboyhatt.jpg",
                   IsDeleted = false,
                   Name = "Cowboyhatt",
                   Price = 2000,
                   Quantity = 1,
                   Size = 12,
                   Depth = 8,
                   Length = 35,
                   Width = 30,
                   HatTypeId = 1
               },
               new Hat
               {
                   Id = 5,
                   Comment = "En lätt och luftig hatt flätad av halm, perfekt för att ge skugga och svalka under soliga sommardagar.",
                   ImageUrl = "Halmhatt.jpg",
                   IsDeleted = false,
                   Name = "Halmhatt",
                   Price = 600,
                   Quantity = 1,
                   Size = 11,
                   Depth = 7,
                   Length = 32,
                   Width = 30,
                   HatTypeId = 1
               });

            builder.Entity<HatMaterial>().HasData(
                // Studenthatt (Id = 1)
                new HatMaterial { HatId = 1, MaterialId = 8, Quantity = 0.5 },  // Bomull
                new HatMaterial { HatId = 1, MaterialId = 12, Quantity = 0.3 }, // Satin
                new HatMaterial { HatId = 1, MaterialId = 22, Quantity = 5 },   // Pärlor
                new HatMaterial { HatId = 1, MaterialId = 21, Quantity = 1 },   // Tygblommor

                // Kaptenshatt (Id = 2)
                new HatMaterial { HatId = 2, MaterialId = 8, Quantity = 0.5 },  // Bomull
                new HatMaterial { HatId = 2, MaterialId = 12, Quantity = 0.3 }, // Satin
                new HatMaterial { HatId = 2, MaterialId = 24, Quantity = 1 },   // Lurextråd

                // Fez (Id = 3)
                new HatMaterial { HatId = 3, MaterialId = 10, Quantity = 0.7 }, // Ull
                new HatMaterial { HatId = 3, MaterialId = 24, Quantity = 0.5 }, // Lurextråd

                // Cowboyhatt (Id = 4)
                new HatMaterial { HatId = 4, MaterialId = 15, Quantity = 1 },   // Läder
                new HatMaterial { HatId = 4, MaterialId = 5, Quantity = 0.5 },  // Palmlöv
                new HatMaterial { HatId = 4, MaterialId = 19, Quantity = 1 },   // Påfågelfjäder

                // Halmhatt (Id = 5)
                new HatMaterial { HatId = 5, MaterialId = 4, Quantity = 0.7 },  // Rishalm
                new HatMaterial { HatId = 5, MaterialId = 21, Quantity = 1 },   // Tygblommor
                new HatMaterial { HatId = 5, MaterialId = 20, Quantity = 2 }    // Hönsfjäder
            );



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
