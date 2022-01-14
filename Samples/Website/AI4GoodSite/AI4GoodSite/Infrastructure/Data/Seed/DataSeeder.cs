using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AI4GoodSite.Infrastructure.Data.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedRoles(ApplicationDbContext context)
        {
            string[] roles = new string[] { "Administrator", "Picker" };
            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole() { Name = role, NormalizedName = role });
                }
            }
        }
    }
}
