using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [DisplayName("Order")]
        public int OrderId { get; set; }
        [DisplayName("Product")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}
