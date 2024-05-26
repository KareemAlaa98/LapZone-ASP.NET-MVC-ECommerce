using E_Commerce_GP.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "QuantityInStock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock can't be negative")]
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Select a Brand")]
        [Required(ErrorMessage = "Brand is required")]
        public int BrandId { get; set; }

        [Display(Name = "Discount (Leave Empty if None)")]
        public int? DiscountId { get; set; }
       
        [ValidateNever]
        public List<ProductImage>? ProductImages { get; set; }

    }
}
