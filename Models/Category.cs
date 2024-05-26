using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;

        public List<Product> Products { get; set; }
    }
}
