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
            var tupleResult = GetNRandomItems(20);
            context.Items.AddRange(tupleResult.Item2);
            context.Orders.AddRange(tupleResult.Item1);
            await context.SaveChangesAsync();
        }

        private static Tuple<List<Order>, List<Item>> GetNRandomItems(int N)
        {
            List<Item> items = new List<Item>();
            List<Order> orders = new List<Order>();
            Random rand = new Random();
            var orderId = 1;
            
            string[] itemNames = new string[] { "Ink Cartridges", "Toner", "Laser Ink Jet cartridges", "Mono Laser Cartridges", "Color Laser cartridges" };
            long sku = 1000000;
            for(int i=0; i<N; ++i)
            {
                var randomIndex = rand.NextInt64(0, 4); // 5 types of items: Int Cartridges, Toner, Laser Ink Jet cartridges, Mono Laser Cartridges, Color Laser cartridges
                var randomItemName = itemNames[randomIndex];
                var newRandomItem = new Item() { ItemId = i+1, Description = randomItemName, Name = randomItemName, SKU = $"{sku++}" };
                items.Add(newRandomItem);
                if(i> 0 && (i+1) % 5 == 0)
                {
                    var newOrder = new Order() { OrderId = orderId++, Status = "Pending", User = "AI4GoodUser", ItemDisplayIdStart = $"{i + 1 - 4}", ItemDisplayIdEnd = $"{i+1}" };
                    orders.Add(newOrder);
                }
            }
            return Tuple.Create(orders, items);
        }
    }
}
