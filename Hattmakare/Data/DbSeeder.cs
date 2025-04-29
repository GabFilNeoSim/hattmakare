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
}
