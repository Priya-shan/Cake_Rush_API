using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Rush_API.Dao
{
    public class DaoCart
    {
        public string userId { get; set; }
        public int mapId { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public int expiry { get; set; }
    }
}
