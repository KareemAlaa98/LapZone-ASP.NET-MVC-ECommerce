using E_Commerce_GP.Models;

namespace E_Commerce_GP.IRepository
{
    public interface IProductImageRepository
    {
        void Create(int productId, string fileName);
        ProductImage Get(int imageId);
        void Delete(int imageId);
        List<ProductImage> GetProductImagesByProductId(int productId);
    }
}
