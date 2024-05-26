using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class FilterViewModel
    {
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Minimum Price")]
        [Range(0, double.MaxValue, ErrorMessage = "The field {0} must be greater than or equal to {1}.")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Maximum Price")]
        [Range(0, double.MaxValue, ErrorMessage = "The field {0} must be greater than or equal to {1}.")]
        public decimal? MaxPrice { get; set; }
    }
}
