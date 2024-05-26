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
        public SearchController(ISearchRepository _searchRepository)
        {
            this.searchRepository = _searchRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(string search)
        {
            var results = searchRepository.SearchProducts(search);
            return View(results);
        }
    }
}
