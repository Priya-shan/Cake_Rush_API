using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Rush_API.Models
{
    public class SubCategoryMapModel
    {
        [Key]
        public int mapId { get; set; }
        //public ProductModel productId { get; set; }

        [ForeignKey("Product")]
        public int productId { get; set; }

        [Required]
        public string categoryName { get; set; }

        [Required]
        public int price { get; set; }

        public virtual ProductModel Product { get; set; }

        //public virtual ICollection<CartModel> carts { get; set; }
    }
}
