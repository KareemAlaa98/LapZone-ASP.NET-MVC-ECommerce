// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using E_Commerce_GP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// [Required, MaxLength(100)]
            public string FirstName { get; set; }

            [Required, MaxLength(100)]
            public string LastName { get; set; }

            [Required, MaxLength(100)]
            public string Street { get; set; }

            [Required, MaxLength(100)]
            public string City { get; set; }

            [Required]
            public int Building_Number { get; set; }

            [Required]
            public int Floor_Number { get; set; }

            [Required, MaxLength(11)]
            public string PhoneNumber { get; set; }
            //[Phone]
            //[Display(Name = "Phone number")]
            //public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Street = user.Street,
                Building_Number = user.Building_Number,
                Floor_Number = user.Floor_Number,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var phoneNumber =user.PhoneNumber;
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var city = user.City;
            var street = user.Street;
            var buildingNumber = user.Building_Number;
            var floorNumber = user.Floor_Number;

            if(Input.FirstName != firstName) 
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);    
            }
            if(Input.LastName != lastName) 
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);    
            }
            if (Input.City != city)
            {
                user.City = Input.City;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Street != street)
            {
                user.Street = Input.Street;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Building_Number != buildingNumber)
            {
                user.Building_Number = Input.Building_Number;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Floor_Number != floorNumber)
            {
                user.Floor_Number = Input.Floor_Number;
                await _userManager.UpdateAsync(user);
            }
            if (Input.PhoneNumber != phoneNumber)
            {
                var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == Input.PhoneNumber);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Phone number is already registered.");
                    return Page();
                }
                user.PhoneNumber = Input.PhoneNumber;
                await _userManager.UpdateAsync(user);
            }
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
