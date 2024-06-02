using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Controllers
{
    public class SearchController : Controller
    {
        ISearchRepository searchRepository;
        IBrandRepository brandRepository;
        public SearchController(ISearchRepository _searchRepository, IBrandRepository _brandRepository)
        {
            this.searchRepository = _searchRepository;
            this.brandRepository = _brandRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string search)
        {
            var results = searchRepository.SearchProducts(search);
            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["searchInput"] = search;
            return View(results);
        }
    }
}
