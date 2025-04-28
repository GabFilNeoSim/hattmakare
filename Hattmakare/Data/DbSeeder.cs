using Microsoft.AspNetCore.Identity;
using Hattmakare.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Data;

public static class DbSeeder
{
    public static async Task SeedAllAsync(
        AppDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        await AddRoles(roleManager);
        await AddUsers(userManager);
        await AddMaterials(context);
        await AddDecorations(context);

        await context.SaveChangesAsync();
    }

    private static async Task AddRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }

    private static async Task AddUsers(UserManager<User> userManager)
    {
        const string password = "password";

        if (await userManager.FindByEmailAsync("otto@hattmakare.se") == null)
        {
            var user1 = new User
            {
                FirstName = "Otto",
                LastName = "Ottosson",
                UserName = "otto@hattmakare.se",
                Email = "otto@hattmakare.se"
            };
            await userManager.CreateAsync(user1, password);
            await userManager.AddToRoleAsync(user1, "Admin");
        }

        if (await userManager.FindByEmailAsync("judiths@hattmakare.se") == null)
        {
            var user2 = new User
            {
                FirstName = "Judiths",
                LastName = "Judithsdottir",
                UserName = "judiths@hattmakare.se",
                Email = "judiths@hattmakare.se"
            };
            await userManager.CreateAsync(user2, password);
            await userManager.AddToRoleAsync(user2, "Admin");
        }
    }

    private static async Task AddMaterials(AppDbContext context)
    {
        List<Material> materials =
        [
            new Material { Name = "Ullfilt", Unit = "m", Price = 124.50m, Supplier = "Nordic Felt AB", IsDecoration = false },
            new Material { Name = "Kaninfilt", Unit = "m", Price = 499.90m, Supplier = "Hatter’s Choice Ltd", IsDecoration = false },
            new Material { Name = "Toquillastrå", Unit = "kg", Price = 419.75m, Supplier = "Ecuador Weaves Co.", IsDecoration = false },
            new Material { Name = "Rishalm", Unit = "kg", Price = 109.20m, Supplier = "StrawCraft Asia", IsDecoration = false },
            new Material { Name = "Palmlöv", Unit = "kg", Price = 94.60m, Supplier = "Tropic Naturals", IsDecoration = false },
            new Material { Name = "Majsblad", Unit = "kg", Price = 134.95m, Supplier = "EcoFiber Mexico", IsDecoration = false },
            new Material { Name = "Hampfibrer", Unit = "kg", Price = 159.50m, Supplier = "GreenTextiles GmbH", IsDecoration = false },
            new Material { Name = "Bomull", Unit = "m", Price = 59.90m, Supplier = "CottonLine Textiles", IsDecoration = false },
            new Material { Name = "Linne", Unit = "m", Price = 84.75m, Supplier = "NordTextil AB", IsDecoration = false },
            new Material { Name = "Ull", Unit = "m", Price = 109.00m, Supplier = "NordTextil AB", IsDecoration = false },
            new Material { Name = "Silke", Unit = "m", Price = 399.90m, Supplier = "Silken Touch Co.", IsDecoration = false },
            new Material { Name = "Satin", Unit = "m", Price = 129.50m, Supplier = "Fabric Elegance", IsDecoration = false },
            new Material { Name = "Tweed", Unit = "m", Price = 179.90m, Supplier = "Highland Textiles", IsDecoration = false },
            new Material { Name = "Polyester", Unit = "m", Price = 49.95m, Supplier = "GlobalPoly Ltd", IsDecoration = false },
            new Material { Name = "Läder", Unit = "st", Price = 849.00m, Supplier = "Scandi Leatherworks", IsDecoration = false },
            new Material { Name = "Lackerat papper", Unit = "st", Price = 0.75m, Supplier = "CraftMaterials.se", IsDecoration = false },
            new Material { Name = "Fuskpäls", Unit = "m", Price = 144.60m, Supplier = "FauxFur Fabrics", IsDecoration = false },
        ];

        foreach (var material in materials)
        {
            if (await context.Materials.Where(m => m.Name == material.Name).SingleOrDefaultAsync() == null)
            {
                await context.Materials.AddAsync(material);
            }
        }
    }

    private static async Task AddDecorations(AppDbContext context)
    {
        List<Material> decorations =
        [
            new Material { Name = "Strutsfjäder", Unit = "st", Price = 35.00m, Supplier = "Bendigo Farm Inc", IsDecoration = true },
            new Material { Name = "Påfågelfjäder", Unit = "st", Price = 64.90m, Supplier = "FeatherWorks Ltd", IsDecoration = true },
            new Material { Name = "Hönsfjäder", Unit = "st", Price = 0.90m, Supplier = "FeatherWorks Ltd", IsDecoration = true },
            new Material { Name = "Tygblommor", Unit = "st", Price = 29.95m, Supplier = "DecoFlora AB", IsDecoration = true },
            new Material { Name = "Pärlor", Unit = "g", Price = 11.50m, Supplier = "Pearl Paradise", IsDecoration = true },
            new Material { Name = "Spets", Unit = "m", Price = 39.90m, Supplier = "Lace & Grace", IsDecoration = true },
            new Material { Name = "Lurextråd", Unit = "m", Price = 59.00m, Supplier = "ShinyThreads Co.", IsDecoration = true }
        ];

        foreach (var decoration in decorations)
        {
            if (await context.Materials.Where(m => m.Name == decoration.Name).SingleOrDefaultAsync() == null)
            {
                await context.Materials.AddAsync(decoration);
            }
        }
    }
}
