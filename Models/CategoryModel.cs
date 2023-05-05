using System.ComponentModel.DataAnnotations;

namespace Cake_Rush_API.Models
{
    public class CategoryModel
    {
        [Key]
        public int categoryId { get; set; }
        [Required]
        public string categoryName { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
