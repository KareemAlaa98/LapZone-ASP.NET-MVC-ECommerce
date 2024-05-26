using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce_GP.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        ApplicationDbContext context;
        IProductRepository productRepository;
        IShoppingCartRepository shoppingCartRepository;
        public CartController(IShoppingCartRepository _shoppingCartRepository, IProductRepository _productRepository, ApplicationDbContext context)
        {
            this.productRepository = _productRepository;
            this.shoppingCartRepository = _shoppingCartRepository;
            this.context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);

            return View(userShoppingCart);
        }

       
        public IActionResult AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingCartRepository.AddProductToCart(userId, id);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);
            var item = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault();
            var quantity = item.Quantity;
            var price = item.Product.Price;
            decimal newPrice = 0;
            if (item.Product.DiscountId != null)
            {
                newPrice = price - (price * item.Product.Discount.DiscountPercent / 100);
            }
            else { newPrice = price; }

            userShoppingCart.CartItems.Remove(item);
            context.SaveChanges();
            userShoppingCart.Total -= (quantity * newPrice);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult IncreaseQuantity(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);
            var item = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault();
            var quantity = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault().Quantity++;
            var price = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault().Product.Price;

            decimal newPrice = 0;
            if (item.Product.DiscountId != null)
            {
                newPrice = price - (price * item.Product.Discount.DiscountPercent / 100);
            }
            else { newPrice = price; }

            context.SaveChanges();
            userShoppingCart.Total += newPrice;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        
        public IActionResult DecreaseQuantity(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);
            var item = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault();
            var quantity = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault().Quantity--;
            var price = userShoppingCart.CartItems.Where(e => e.Id == id).FirstOrDefault().Product.Price;
            
            decimal newPrice = 0;
            if (item.Product.DiscountId != null)
            {
                newPrice = price - (price * item.Product.Discount.DiscountPercent / 100);
            }
            else { newPrice = price; }
            context.SaveChanges();

            userShoppingCart.Total -= newPrice;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);
            return View(userShoppingCart);
        }
    }
}
