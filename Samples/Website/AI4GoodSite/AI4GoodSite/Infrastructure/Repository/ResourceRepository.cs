using System;
using System.Collections.Generic;
using System.Linq;
using AI4GoodSite.Infrastructure.Data;
using AI4GoodSite.Infrastructure.Repository.Interface;

namespace AI4GoodSite.Infrastructure.Repository
{
    public class ResourceRepository:IResourceRepository
    {
        ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Item> GetItemsList()
        {
            return _context.Items.ToList();
        }
    }
}
