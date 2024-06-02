using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;
        IProductRepository productRepository;

        public WishlistRepository(ApplicationDbContext context, IProductRepository _productRepository)
        {
            _context = context;
            this.productRepository = _productRepository;
        }

        public Wishlist GetWishlist(string userId)
        {
            // If No User is Signed in (this check is for the code logic of loading the wishlist at the top of _layout page
            if (userId == null)
            {
                return null;
            }

            var userWishlist = _context.Wishlists
                .Include(w => w.Products)
                    .ThenInclude(e=>e.ProductImages)
                .Include(w => w.Products)
                    .ThenInclude(e => e.Discount)
                .Where(w => w.ApplicationUserId == userId)
                .FirstOrDefault();

            if(userWishlist == null)
            {
                userWishlist = new Wishlist
                {
                    ApplicationUserId = userId,
                    Products = new List<Product>()
                };
                _context.Wishlists.Add(userWishlist);
                _context.SaveChanges();
            }
            else
            {
                // Filter out deleted products after loading the wishlist
                userWishlist.Products = userWishlist.Products.Where(p => !p.IsDeleted).ToList();
            }

            return userWishlist; 
        }

        public void AddProduct(string userId, int productId)
        {
            var product = productRepository.ReadById(productId);
            var wishlist = GetWishlist(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    ApplicationUserId = userId,
                    Products = new List<Product>()
                };
                _context.Wishlists.Add(wishlist);
                _context.SaveChanges();
            }

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
