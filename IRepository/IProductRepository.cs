using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;

namespace E_Commerce_GP.IRepository
{
    public interface IProductRepository
    {
        //Read
        List<Product> ReadAll();

        Product ReadById(int id);

        void Delete(int id);

        //Update
        void Update(ProductViewModel productVM);


        //Create
        Product Create(ProductViewModel productVM);
        Product GetProductByName(string name);


        //Filter
        List<Product> Filter(string? brandName = null, decimal? minPrice = null, decimal? maxPrice = null, int? rating = null);
        decimal RecalculateAverageRating(int productId);

    }
}
