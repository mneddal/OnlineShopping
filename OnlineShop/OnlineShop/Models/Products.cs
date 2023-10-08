using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Products
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Available")]
        public bool IsAvaiable { get; set; }
        [Display(Name = "Product Color")]
        public string ProductColor {  get; set; }
        [Required]
        [Display(Name="Product Type")]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes? ProductTypes { get; set; }
    }
}
