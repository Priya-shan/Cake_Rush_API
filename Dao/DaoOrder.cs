namespace Cake_Rush_API.Dao
{
    public class DaoOrder
    {
        public string userId { get; set; }
        public int cartId { get; set; }

        public string message { get; set; }

        public int amount { get; set; }

        public string orderStatus { get; set; }

        public DateTime dateOrdered { get; set; }
        public string deliveryMode { get; set;}

        public string paymentMode { get; set; }
    }
}
