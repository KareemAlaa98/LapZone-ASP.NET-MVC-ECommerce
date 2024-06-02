using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }


        public List<Product> GetDeletedProducts()
        {
            return context.Products
                .Include(e => e.Brands)
                .Include(c => c.Category)
                .Include(v => v.Discount)
                .Include(i => i.ProductImages)
                .Where(e => e.IsDeleted)
                .ToList();
        }

        //Read
        public List<Product> ReadAll()
        {
            return context.Products
                .Include(e => e.Brands)
                .Include(c => c.Category)
                .Include(v => v.Discount)
                .Include(i=>i.ProductImages)
                .Where(e => !e.IsDeleted)
                .ToList();
        }
        public Product ReadById(int id)
        {
            return context.Products.Include(e=>e.Discount).Include(e=>e.ProductImages).Include(e => e.Reviews).ThenInclude(e=>e.ApplicationUser).FirstOrDefault(e=>e.Id == id);
        }


        //Create
        public Product Create(ProductViewModel productVM)
        {
            var product = new Product();
            product.Id = productVM.Id;
            product.Name = productVM.Name;
            product.Description = productVM.Description;
            product.Price = productVM.Price;
            product.QuantityInStock = productVM.QuantityInStock;
            product.BrandId = productVM.BrandId;
            product.DiscountId = productVM.DiscountId;
            context.Products.Add(product);
            context.SaveChanges();

            return product;

        }

        //Update
        public void Update(ProductViewModel productVM)
        {
            var dbProduct = ReadById(productVM.Id);
            if (dbProduct != null)
            {
                dbProduct.Name = productVM.Name;
                dbProduct.Description = productVM.Description;
                dbProduct.Price = productVM.Price;
                dbProduct.QuantityInStock = productVM.QuantityInStock;
                dbProduct.BrandId = productVM.BrandId;
                dbProduct.DiscountId = productVM.DiscountId;
                dbProduct.ModifiedAt = DateTime.Now;
                context.SaveChanges();
            }
        }

        //Delete
        public void Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                product.IsDeleted = true;
                context.SaveChanges();
            }
        }
        public void Restore(int id)
        {
            var product = context.Products.Include(e=>e.Brands).FirstOrDefault(e=>e.Id == id);
            if (product != null)
            {
                if (product.Brands.IsDeleted)
                {
                    throw new Exception("The Brand of this product is deleted, you Must restore it first.");
                }
                else
                {
                    product.IsDeleted = false;
                    context.SaveChanges();
                }
            }
        }

        public List<Product> Filter(string? brandName = null, decimal? minPrice = null, decimal? maxPrice = null, int? rating = null)
        {
            var query = context.Products
                .Include(e => e.Discount)
                .Include(e => e.Brands)
                .Include(e => e.ProductImages)
                .Where(e => !e.IsDeleted)
                .AsQueryable();

            // Brand Filter
            if (!string.IsNullOrEmpty(brandName))
            {
                query = query.Where(e => e.Brands.Name == brandName);
            }

            // Rating Filter
            if (rating.HasValue)
            {
                query = query.Where(e => e.AverageRating >= rating && e.AverageRating < rating + 1);
            }

            // Price Filter
            var products = query.ToList();

            // Apply price filters
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(e => e.DiscountedPrice >= minPrice.Value && e.DiscountedPrice <= maxPrice.Value).ToList();
            }
            else if (minPrice.HasValue)
            {
                products = products.Where(e => e.DiscountedPrice >= minPrice.Value).ToList();
            }
            else if (maxPrice.HasValue)
            {
                products = products.Where(e => e.DiscountedPrice <= maxPrice.Value).ToList();
            }

            return products;
        }


        public decimal RecalculateAverageRating(int productId)
        {
            var product = context.Products.Include(p => p.Reviews).FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                if (product.Reviews != null && product.Reviews.Any())
                {
                    decimal totalRating = product.Reviews.Sum(r => r.Rating);
                    decimal averageRating = totalRating / product.Reviews.Count;

                    product.AverageRating = averageRating;
                    context.SaveChanges();
                    return averageRating;
                }
            }
            return 0; 
        }




    }
}
