using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_GP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IProductRepository productRepository;
        IBrandRepository brandRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IBrandRepository _brandRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
            this.brandRepository = _brandRepository;
        }

        public IActionResult Index()
        {
            var allProducts = productRepository.ReadAll();
            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            return View(allProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
