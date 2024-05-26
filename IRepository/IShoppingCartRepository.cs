using E_Commerce_GP.Models;

namespace E_Commerce_GP.IRepository
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetUserShoppingCart(string userId);
        void AddProductToCart(string userId, int productId);
    }
}
