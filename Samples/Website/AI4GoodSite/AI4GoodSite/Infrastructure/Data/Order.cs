using System.ComponentModel.DataAnnotations.Schema;

namespace AI4GoodSite.Infrastructure.Data
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int ItemIdStart { get; set; }
        public int ItemIdEnd { get; set; }
    }
}
