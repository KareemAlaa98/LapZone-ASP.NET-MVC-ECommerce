using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class DiscountViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Discount Percent")]
        [Range(1, 99)]
        public int DiscountPercent { get; set; }
    }
}
