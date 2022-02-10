using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AI4GoodSite.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private string dbSchema = "AI4Good";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(dbSchema);
            base.OnModelCreating(builder);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemScan> ItemScans { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
