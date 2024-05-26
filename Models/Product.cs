using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public decimal Price {  get; set; }

        [Required]
        public int QuantityInStock { get; set;}

        public decimal AverageRating { get; set; } = 0;

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brands { get; set; }

        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }

        public List<Wishlist> Wishlists { get; set; }
        
        public List<ProductImage> ProductImages { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
