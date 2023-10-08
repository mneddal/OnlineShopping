using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Order Number")]
        public string OrderNo {  get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime OrderDate { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }


    }
}
