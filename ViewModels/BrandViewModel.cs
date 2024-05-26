using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Brand name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }

}
