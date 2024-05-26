using E_Commerce_GP.Data;
using E_Commerce_GP.Models;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class BrandRepository : IBrandRepository
    {

        ApplicationDbContext context;

        public BrandRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        //Delete
        public void Delete(int id)
        {
            var brand = ReadByIdBrand(id);
            if (brand != null)
            {
                context.Brands.Remove(brand);
                context.SaveChanges();
            }
        }

        //Update
        public void UpdateBrand(BrandViewModel brandVM)
        {
            var Brand = context.Brands.Find(brandVM.Id);
            if (Brand != null)
            {
                Brand.Name = brandVM.Name;
                context.SaveChanges();
            }
        }

        //Read
        public List<Brand> ReadAllBrand()
        {
            return context.Brands.ToList();
        }

        public Brand ReadByIdBrand(int id)
        {
            return context.Brands.Include(e=>e.Products).FirstOrDefault(e=>e.Id == id);
        }

        //Create
        public void CreateBrand(BrandViewModel brandVM)
        {
            var brand = new Brand();
            brand.Name = brandVM.Name;
            context.Brands.Add(brand);
            context.SaveChanges();
        }

        public List<Product> GetProductsInBrand(int id)
        {
            return context.Products.Include(e => e.Brands)
                .Include(c => c.Category)
                .Include(v => v.Discount)
                .Include(i => i.ProductImages)
                .Where(e => e.BrandId == id).ToList();
        }
    }

}

