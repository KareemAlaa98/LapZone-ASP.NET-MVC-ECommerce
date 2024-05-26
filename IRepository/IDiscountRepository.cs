using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
namespace E_Commerce_GP.IRepository
{
    public interface IDiscountRepository
    {
        List<Discount> ReadAll();
        Discount ReadById(int id);

        void Create(DiscountViewModel discountVM);
        void Update(DiscountViewModel discountVM);
        void Delete(int id);
        //List<Product> getProductOfDiscount(int id);
    }
}
