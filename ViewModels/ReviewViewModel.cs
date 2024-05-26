using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
