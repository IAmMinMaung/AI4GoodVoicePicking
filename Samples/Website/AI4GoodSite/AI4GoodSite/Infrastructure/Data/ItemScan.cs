using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI4GoodSite.Infrastructure.Data
{
    public class ItemScan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ItemScanId { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime ScannedDateTime { get; set; }
        public string User { get; set; }
    }
}
