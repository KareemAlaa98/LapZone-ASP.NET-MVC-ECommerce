using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.Repository;
using E_Commerce_GP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_GP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository _discountRepository)
        {
            this._discountRepository = _discountRepository;

        }
        public IActionResult Index()
        {

            List<Discount> discounts = _discountRepository.ReadAll();
            return View(discounts);

        }
        public IActionResult Create()
        {
            var discountvm = new DiscountViewModel();
            return View(discountvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DiscountViewModel discountvm)
        {
            if (ModelState.IsValid)
            {
                _discountRepository.Create(discountvm);

                return RedirectToAction("Index");
            }
            return View("create", discountvm);

        }
        public IActionResult Edit(int id)
        {
            var discount = _discountRepository.ReadById(id);
            if (discount == null)
            {
                return RedirectToAction("Index");
            }
            var discountVM = new DiscountViewModel
            {
                Id = discount.Id,
                Name = discount.Name,
                Description = discount.Description,
                DiscountPercent = discount.DiscountPercent
            };
            return View(discountVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DiscountViewModel discVM)
        {
            if (ModelState.IsValid)
            {
                _discountRepository.Update(discVM);

                return RedirectToAction("Index");
            }
            return View("Edit", discVM);

        }


        public IActionResult Delete(int? id)
        {
            Discount discountFromDb = _discountRepository.ReadById(id.Value);

            if (discountFromDb == null)
            {
                return NotFound();
            }

            DiscountViewModel discountViewModel = new DiscountViewModel
            {
                Id = discountFromDb.Id,
                Name = discountFromDb.Name,
                Description = discountFromDb.Description,
                DiscountPercent = discountFromDb.DiscountPercent,
                
            };

            return View(discountViewModel);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var discountFromDb = _discountRepository.ReadById(id);

            if (discountFromDb == null)
            {
                return NotFound();
            }

            _discountRepository.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
