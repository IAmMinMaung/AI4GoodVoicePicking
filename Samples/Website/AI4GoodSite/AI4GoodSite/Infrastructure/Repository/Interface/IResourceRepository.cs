using System;
using System.Collections.Generic;
using AI4GoodSite.Infrastructure.Data;

namespace AI4GoodSite.Infrastructure.Repository.Interface
{
    public interface IResourceRepository
    {
        List<Item> GetItemsList();
    }
}
