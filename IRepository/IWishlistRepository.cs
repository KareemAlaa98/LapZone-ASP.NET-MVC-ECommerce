using E_Commerce_GP.Models;

namespace E_Commerce_GP.IRepository
{
    public interface IWishlistRepository
    {
        Wishlist GetWishlist(string userId);
        void AddProduct(string userId, int productId);
        void RemoveProduct(string userId, int productId);
    }
}
