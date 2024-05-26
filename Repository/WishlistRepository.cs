using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Wishlist GetWishlist(string userId)
        {
            return _context.Wishlists
                .Include(w => w.Products)
                    .ThenInclude(e=>e.ProductImages)
                .Include(w => w.Products)
                    .ThenInclude(e => e.Discount)
                .FirstOrDefault(w => w.ApplicationUserId == userId);
        }

        public void AddProduct(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            }

            var wishlist = GetWishlist(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    ApplicationUserId = userId,
                    Products = new List<Product>()
                };
                _context.Wishlists.Add(wishlist);
            }

            var product = _context.Products.Find(productId);
            wishlist.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(string userId, int productId)
        {
            var wishlist = GetWishlist(userId);
            if (wishlist != null)
            {
                var productToRemove = wishlist.Products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    wishlist.Products.Remove(productToRemove);
                    _context.SaveChanges();
                }
            }
        }
    }
}
