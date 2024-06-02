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

        public List<Brand> GetDeletedBrands()
        {
            return context.Brands
                .Where(e => e.IsDeleted)
                .ToList();
        }
        
        public List<Product> GetProductsOfDeletedBrands(int id)
        {
            return context.Products.Where(e => e.BrandId == id).ToList();
        }

        //Delete
        public void Delete(int id)
        {
            var brand = ReadByIdBrand(id);
            if (brand != null)
            {
                brand.IsDeleted = true;
                foreach(var product in brand.Products)
                {
                    product.IsDeleted = true;
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
        }

        public void Restore(int id)
        {
            var brand = ReadByIdBrand(id);
            if (brand != null)
            {
                brand.IsDeleted = false;
                foreach (var product in brand.Products)
                {
                    product.IsDeleted = false;
                    context.SaveChanges();
                }
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
            return context.Brands
                .Where(e => !e.IsDeleted)
                .ToList();
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

