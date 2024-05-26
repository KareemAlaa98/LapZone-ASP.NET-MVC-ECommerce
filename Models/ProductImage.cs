using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models 
{ 
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
