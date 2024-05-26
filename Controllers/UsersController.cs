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

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                // Get the role(s) of each User
                var roles = await userManager.GetRolesAsync(user);

                var userViewModel = new UserViewModel
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
        public async Task<IActionResult> ManageRoles(UserViewModel userVM)
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

            var userVM = new UserViewModel()
            {
                //Roles = roles
            };
            ViewData["listOfRoles"] = roles;
            return View(userVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveNew(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
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

                var user = new ApplicationUser
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
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

                await userManager.AddToRolesAsync(user, userVM.Roles.Select(e=>e.RoleName).ToArray());


                return RedirectToAction("Index");
            }


            var roles = roleManager.Roles.Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToList();
            ViewData["listOfRoles"] = roles;
            return View("Add", userVM );
        }

    }
}
