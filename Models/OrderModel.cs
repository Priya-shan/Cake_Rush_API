using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Rush_API.Models
{
    public class OrderModel
    {
        [Key]
        public int orderId { get; set; }

        public string userId { get; set; }

        [ForeignKey("Cart")]
        public int cartId { get; set; }

        public string message { get; set; }

        public int amount { get; set; }

        public string orderStatus { get; set; }

        public DateTime dateOrdered { get; set; }
        public string deliveryMode { get; set;}

        public string paymentMode { get; set; }

        public virtual CartModel Cart { get; set; }
    }
}
