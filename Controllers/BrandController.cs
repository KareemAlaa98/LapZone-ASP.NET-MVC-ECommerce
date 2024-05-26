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
        IBrandRepository BrandRepository;
        public BrandController(IBrandRepository brandIRepository)
        {
            this.BrandRepository = brandIRepository;
        }

        [AllowAnonymous]
        public IActionResult ShowBrands()
        {
            var brands = BrandRepository.ReadAllBrand();

            return View("ShowBrands", brands);
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
                BrandRepository.CreateBrand(newbrand);
                return RedirectToAction("ShowBrands");
            }
            return View("Create", newbrand);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = BrandRepository.ReadByIdBrand(id);
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
                BrandRepository.UpdateBrand(brandVM);
                return RedirectToAction("ShowBrands");
            }
            return View("Edit", brandVM);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var brandProducts = BrandRepository.GetProductsInBrand(id);
            if (brandProducts == null)
            {
                return View("NotFound");
            }
            return View(brandProducts);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            BrandRepository.Delete(id);
            return RedirectToAction("ShowBrands");
        }


    }
}
