using System.ComponentModel.DataAnnotations;


namespace E_Commerce_GP.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }

        [Required, MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters (lowercase or uppercase) are allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters (lowercase or uppercase) are allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required, MaxLength(100)]
        public string Street { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }


        [Required]
        [Display(Name = "Building Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Building Number Can't be negative")]
        public int Building_Number { get; set; }

        [Required]
        [Display(Name = "Floor Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Floor Number Can't be negative")]
        public int Floor_Number { get; set; }

        [Required, MaxLength(11)]
        [RegularExpression(@"^01[0-2|5][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }
    }
}
