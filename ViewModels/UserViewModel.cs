using E_Commerce_GP.Models;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_GP.ViewModels
{
    public class UserViewModel
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


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


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

        public IEnumerable<string>? UserRoles { get; set; } 
        // if we user List instead of IEnumerable we'll get this error for the roles in the controller's index action
        // when retrieving the current user roles and will have to implicitly cast the result (List<string>).
        // THE ERROR: Cannot implicitly convert type 'System.Collections.Generic.IList<string>' to 'System.Collections.Generic.List<string>'.
        //            An explicit conversion exists (are you missing a cast?)

        public List<RoleViewModel> Roles { get; set; }


    }
}
