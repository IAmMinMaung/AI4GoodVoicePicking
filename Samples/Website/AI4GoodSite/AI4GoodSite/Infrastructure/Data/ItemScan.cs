using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI4GoodSite.Infrastructure.Data
{
    public class ItemScan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemScanId { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int DisplayId { get; set; }
        public Item Item { get; set; }
        public DateTime ScannedDateTime { get; set; }
        public string User { get; set; }
    }
}
