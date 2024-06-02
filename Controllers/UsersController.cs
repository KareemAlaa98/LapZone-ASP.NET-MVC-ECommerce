using E_Commerce_GP.Data;
using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        ApplicationDbContext context= new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> userManager;   
        private readonly RoleManager<IdentityRole> roleManager;   
        public UsersController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) 
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();

            var userViewModels = new List<AddUserViewModel>();
            foreach (var user in users)
            {
                // Get the role(s) of each User
                var roles = await userManager.GetRolesAsync(user);

                var userViewModel = new AddUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City,
                    Street = user.Street,
                    Building_Number = user.Building_Number,
                    Floor_Number = user.Floor_Number,
                    UserRoles = roles
                };
                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await roleManager.Roles.ToListAsync();
            var userRolesVM = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };
            return View(userRolesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(AddUserViewModel userVM)
        {
            var user = await userManager.FindByIdAsync(userVM.Id);
            if (user == null) return NotFound();

            var userRoles = await userManager.GetRolesAsync(user);

            foreach(var role in userVM.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
                
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await userManager.AddToRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.Select(r => new RoleViewModel{RoleId = r.Id, RoleName = r.Name}).ToListAsync();

            var userVM = new AddUserViewModel()
            {
                Roles = roles
            };
            ViewData["listOfRoles"] = roles;
            return View(userVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNew(AddUserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", userVM);
            }

            if (!userVM.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please Select at least one role");
                return View("Add", userVM);
            }

            if(await userManager.FindByNameAsync(userVM.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Username already Exists");
                return View("Add", userVM );
            }

            if(await userManager.FindByEmailAsync(userVM.Email) != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View("Add", userVM );
            }

            var existingUserWithPhoneNumber = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == userVM.PhoneNumber);

            if (existingUserWithPhoneNumber != null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists");
                return View("Add", userVM);
            }


            var user = new ApplicationUser
            {
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Email = userVM.Email,
                EmailConfirmed = true,
                UserName = userVM.UserName,
                City = userVM.City,
                Street = userVM.Street,
                Building_Number = userVM.Building_Number,
                Floor_Number = userVM.Floor_Number,
                PhoneNumber = userVM.PhoneNumber,
                CreatedAt = DateTime.Now
            };
            var result = await userManager.CreateAsync(user, userVM.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View("Add", userVM );
            }

            await userManager.AddToRolesAsync(user, userVM.Roles.Where(r=>r.IsSelected).Select(e=>e.RoleName));


            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userVM = new EditUserViewModel() 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Street = user.Street,
                City = user.City,
                PhoneNumber = user.PhoneNumber
            };

            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(EditUserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", userVM);
            }

            var user = await userManager.FindByIdAsync(userVM.Id);
            if (user == null) return NotFound();

            
            var userWithEmailAlreadyExists = await userManager.FindByEmailAsync(userVM.Email);
            if(userWithEmailAlreadyExists != null && userWithEmailAlreadyExists.Id != user.Id)
            {
                ModelState.AddModelError("Email", "This email is already assigned to another user");
                return View("Edit", userVM);
            }    
            
            var userWithUserNameAlreadyExists = await userManager.FindByNameAsync(userVM.UserName);
            if (userWithUserNameAlreadyExists != null && userWithUserNameAlreadyExists.Id != user.Id)
            {
                ModelState.AddModelError("UserName", "This username is already assigned to another user");
                return View("Edit", userVM);
            }

            var userWithPhoneNumberAlreadyExists = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == userVM.PhoneNumber);
            if (userWithPhoneNumberAlreadyExists != null && userWithPhoneNumberAlreadyExists.Id != user.Id)
            {
                ModelState.AddModelError("PhoneNumber", "This Phone Number is already assigned to another user");
                return View("Edit", userVM);
            }

            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Email = userVM.Email;
            user.EmailConfirmed = true;
            user.UserName = userVM.UserName;
            user.City = userVM.City;
            user.Street = userVM.Street;
            user.Building_Number = userVM.Building_Number;
            user.Floor_Number = userVM.Floor_Number;
            user.PhoneNumber = userVM.PhoneNumber;
            user.ModifiedAt = DateTime.Now;
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(string id)
        {
            var user= await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) 
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

    }
}
