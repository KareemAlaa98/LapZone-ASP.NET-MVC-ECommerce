using E_Commerce_GP.Models;

namespace E_Commerce_GP.IRepository
{
    public interface ISearchRepository
    {
        List<Product> SearchProducts(string search);
    }
}
