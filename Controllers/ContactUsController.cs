using Microsoft.AspNetCore.Mvc;
using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Repository;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_GP.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsRepository contactUsRepository;
        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            this.contactUsRepository = contactUsRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<ContactUs> contactUs = contactUsRepository.ReadAll();
            return View(contactUs);
        }
        public IActionResult Create()
        {
            var contactUsVM = new ContactUsViewModel();
            return View(contactUsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactUsViewModel contactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                contactUsRepository.Create(contactUsViewModel);
                return RedirectToAction("Index", "Home");

            }

            return View("Create", contactUsViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(contactUsRepository.ReadById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            contactUsRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
