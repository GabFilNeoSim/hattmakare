using Microsoft.AspNetCore.Identity;
using Hattmakare.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hattmakare.Data;

public static class DbSeeder
{
    public static async Task SeedAllAsync(AppDbContext context, UserManager<User> userManager)
    {
        await AddUsers(userManager);
        await AddHats(context);

        await context.SaveChangesAsync();
    }

    private static async Task AddUsers(UserManager<User> userManager)
    {
        string password = "password";

        if (await userManager.FindByEmailAsync("otto@hattmakare.se") == null)
        {
            await userManager.CreateAsync(new User
            {
                FirstName = "Otto",
                LastName = "Ottosson",
                UserName = "otto@hattmakare.se",
                Email = "otto@hattmakare.se"
            }, password);
        }

        if (await userManager.FindByEmailAsync("judiths@hattmakare.se") == null)
        {
            await userManager.CreateAsync(new User
            {
                FirstName = "Judiths",
                LastName = "Judithsdottir",
                UserName = "judiths@hattmakare.se",
                Email = "judiths@hattmakare.se"
            }, password);
        }
    }

    private static async Task AddHats(AppDbContext context)
    {
        if (await context.Hats.SingleOrDefaultAsync(hat => hat.Name == "Bästa hatten") == null)
        {
            await context.Hats.AddAsync(new Hat
            {
                Name = "Bästa hatten",
                Price = 100
            });
        }

        if (await context.Hats.SingleOrDefaultAsync(hat => hat.Name == "Fetaste hatten") == null)
        {
            await context.Hats.AddAsync(new Hat
            {
                Name = "Fetaste hatten",
                Price = 150
            });
        }
    }
}
