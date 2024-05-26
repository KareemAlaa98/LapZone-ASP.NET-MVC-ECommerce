using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters (lowercase or uppercase) and spaces are allowed.")]
        public string Name { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Please enter a valid email address. Example: john.doe@example.com")]
        public string Email { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
        [Required]
        public string Subject { get; set; } = null!;
        public bool IsHandled { get; set; } = false;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;
    }
}

