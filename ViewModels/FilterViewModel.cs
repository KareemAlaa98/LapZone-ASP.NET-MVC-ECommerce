using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class FilterViewModel
    {
        [Display(Name = "Brand Name")]
        public List<string> BrandNames { get; set; }

    }
}
