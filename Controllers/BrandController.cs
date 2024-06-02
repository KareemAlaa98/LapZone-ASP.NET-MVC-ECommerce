using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_GP.Controllers
{
    public class BrandController : Controller
    {
        IBrandRepository brandRepository;
        public BrandController(IBrandRepository brandIRepository)
        {
            this.brandRepository = brandIRepository;
        }

        [AllowAnonymous]
        public IActionResult ShowBrands()
        {
            var brands = brandRepository.ReadAllBrand();
            ViewData["actionUrl"] = "ShowBrands";
            return View("ShowBrands", brands);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ShowDeleted()
        {
            ViewData["actionUrl"] = "ShowDeleted";
            var deletedBrands = brandRepository.GetDeletedBrands();
            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            return View(deletedBrands);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new BrandViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(BrandViewModel newbrand)
        {
            if (ModelState.IsValid)
            {
                brandRepository.CreateBrand(newbrand);
                return RedirectToAction("ShowBrands");
            }
            return View("Create", newbrand);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = brandRepository.ReadByIdBrand(id);
            if (brand == null)
            {
                return RedirectToAction("ShowBrands");
            }
            BrandViewModel BrandVM = new BrandViewModel();

            BrandVM.Id = brand.Id;
            BrandVM.Name = brand.Name;

            return View(BrandVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BrandViewModel brandVM)
        {
            if (ModelState.IsValid)
            {
                brandRepository.UpdateBrand(brandVM);
                return RedirectToAction("ShowBrands");
            }
            return View("Edit", brandVM);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var brandProducts = brandRepository.GetProductsInBrand(id);
            if (brandProducts == null)
            {
                return View("NotFound");
            }
            var brand = brandRepository.ReadByIdBrand(id);
            if (brand.IsDeleted)
            {
                ViewData["brandStatus"] = "deleted";
            }
            else { ViewData["brandStatus"] = "exists"; }

            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            return View(brandProducts);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            brandRepository.Delete(id);
            return RedirectToAction("ShowBrands");
        }

        
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
           brandRepository.Restore(id);
           return RedirectToAction("ShowBrands"); 
        }


    }
}
