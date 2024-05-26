using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;

namespace E_Commerce_GP.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        ApplicationDbContext context;
        public DiscountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Discount> ReadAll()
        {
            return context.Discounts.ToList();
        }
        public Discount ReadById(int id)
        {
            return context.Discounts.Find(id);
        }
        public void Create(DiscountViewModel discountVM)
        {
            var discount = new Discount();
            discount.Id = discountVM.Id;
            discount.Name = discountVM.Name;
            discount.Description = discountVM.Description;
            discount.DiscountPercent = discountVM.DiscountPercent;
            context.Discounts.Add(discount);
            context.SaveChanges();
        }
        public void Update(DiscountViewModel discountVM)
        {
            var discountupdate = ReadById(discountVM.Id);
            if (discountupdate != null)
            {
                discountupdate.Id = discountVM.Id;
                discountupdate.Name = discountVM.Name;
                discountupdate.Description = discountVM.Description;
                discountupdate.DiscountPercent = discountVM.DiscountPercent;
                discountupdate.ModifiedAt = DateTime.Now;
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var discountremove = ReadById(id);
            if (discountremove != null)
            {
                context.Discounts.Remove(discountremove);
                context.SaveChanges();
            }
        }

        //public List<Product> getProductOfDiscount(int id)
        //{
        //    var discount = context.Discounts.Include(d => d.Products).FirstOrDefault(d => d.Id == id);
        //    if (discount == null)
        //        return null;

        //    return discount.Products.ToList();
        //}

    }
}
