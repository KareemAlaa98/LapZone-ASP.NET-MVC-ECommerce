using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Discount Percent")]
        [Range(0, 99)]
        public int DiscountPercent { get; set; }


        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;
    }
}
