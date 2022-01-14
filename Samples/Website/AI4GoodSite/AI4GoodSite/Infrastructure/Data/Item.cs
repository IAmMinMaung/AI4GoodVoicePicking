using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI4GoodSite.Infrastructure.Data
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
    }
}
