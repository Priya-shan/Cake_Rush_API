using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Rush_API.Models
{
    public class CartModel
    {
        [Key]
        public int cartId { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        [ForeignKey("SubCatMap")]
        public int mapId { get; set; }

        public int quantity { get; set; }
        public int price { get; set; }

        public int expiry { get; set; }
        public virtual UserModel User { get; set; }

        public virtual SubCategoryMapModel SubCatMap { get; set; }

    }
}
