using System;
using System.Collections.Generic;
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

        public static async Task SeedItems(ApplicationDbContext context)
        {
            var item = new Item() { ItemId = Guid.NewGuid(), Description = "Ink Cartridge", Name = "Ink Cartridge", SKU = "SKU TBD" };
            var itemList = new List<Item>();
            itemList.AddRange(GetNRandomItems(20));
            context.Items.AddRange(itemList);
            await context.SaveChangesAsync();
        }

        private static List<Item> GetNRandomItems(int N)
        {
            List<Item> items = new List<Item>();
            Random rand = new Random();
            
            string[] itemNames = new string[] { "Ink Cartridges", "Toner", "Laser Ink Jet cartridges", "Mono Laser Cartridges", "Color Laser cartridges" };
            for(int i=0; i<N; ++i)
            {
                var randomIndex = rand.NextInt64(0, 4); // 5 types of items: Int Cartridges, Toner, Laser Ink Jet cartridges, Mono Laser Cartridges, Color Laser cartridges
                var randomItemName = itemNames[randomIndex];
                var newRandomItem = new Item() { ItemId = Guid.NewGuid(), Description = randomItemName, Name = randomItemName, SKU = "SKU TBD" };
                items.Add(newRandomItem);
            }
            return items;
        }
    }
}
