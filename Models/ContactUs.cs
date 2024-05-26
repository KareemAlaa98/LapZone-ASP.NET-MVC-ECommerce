using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;  
        public string Message { get; set; } = null!;
        public bool IsHandled { get; set; } = false;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;
    }
}
