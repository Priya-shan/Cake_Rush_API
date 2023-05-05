using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake_Rush_API.Models
{
    public class ProductModel
    {
        [Key]
        public int productId { get; set; }

        [ForeignKey("Category")]
        public int categoryId { get; set; }

        [Required]
        public string productName { get; set; }

        [Required]
        public string productDescription { get; set; }

        [Required]
        public string label { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public string imageId { get; set; }

        public virtual CategoryModel Category { get; set; }

        //public virtual ICollection<SubCategoryMapModel> SubCategoryMap { get; set; }
    }
}
