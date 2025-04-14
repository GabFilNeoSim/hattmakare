using Microsoft.AspNetCore.Identity;
using Hattmakare.Data.Entities;

namespace Hattmakare.Data;

public static class DbSeeder
{
    public static async Task SeedAllAsync(AppDbContext context, UserManager<User> userManager)
    {
        await AddUsers(userManager);
    }

    private static async Task AddUsers(UserManager<User> userManager)
    {
        string password = "password";

        if (await userManager.FindByEmailAsync("otto@hattmakare.se") == null)
        {
            await userManager.CreateAsync(new User
            {
                UserName = "otto@hattmakare.se",
                Email = "otto@hattmakare.se"
            }, password);
        }

        if (await userManager.FindByEmailAsync("judiths@hattmakare.se") == null)
        {
            await userManager.CreateAsync(new User
            {
                UserName = "judiths@hattmakare.se",
                Email = "judiths@hattmakare.se"
            }, password);
        }
    }
}
