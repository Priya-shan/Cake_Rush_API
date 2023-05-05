using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cake_Rush_API.Models
{
    public class UserModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string userId { get; set; }

        public string? userName { get; set; }

        public string? email { get; set; }

        public string? mobile { get; set; }

        public string? address { get; set; }

        public string? city { get; set; }
        public string? pincode { get; set; }

        //public virtual ICollection<CartModel> carts { get; set; }



    }
}
