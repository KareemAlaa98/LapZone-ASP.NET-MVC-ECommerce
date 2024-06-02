using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;

namespace E_Commerce_GP.IRepository
{
    public interface IBrandRepository
    {
        void Delete(int id);

        //Update
        void UpdateBrand(BrandViewModel brand);

        //Read
        List<Brand> ReadAllBrand();

        Brand ReadByIdBrand(int id);

        //Create
        void CreateBrand(BrandViewModel brand);

        List<Product> GetProductsInBrand(int id);

        List<Brand> GetDeletedBrands();
        void Restore(int id);

        List<Product> GetProductsOfDeletedBrands(int id);
    }
}
