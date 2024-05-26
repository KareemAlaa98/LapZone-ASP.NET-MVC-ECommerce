using E_Commerce_GP.IRepository;
using E_Commerce_GP.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_GP.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        IWishlistRepository WishlistRepository;
        public WishlistController(IWishlistRepository WishlistRepository)
        {
            this.WishlistRepository = WishlistRepository;
        }

       
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = WishlistRepository.GetWishlist(userId);
            return View(wishlist);
        }
       
        public IActionResult AddProductToWishlist(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            WishlistRepository.AddProduct(userId, id);
            return RedirectToAction("Index"); 
        }

        
        public IActionResult RemoveProduct(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            WishlistRepository.RemoveProduct(userId, productId);
            return RedirectToAction("Index");
        }
    }
}
