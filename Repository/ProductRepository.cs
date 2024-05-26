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


        public Product GetProductByName(string name)
        {
            return context.Products.FirstOrDefault(e => e.Name == name);
        }

        //Read
        public List<Product> ReadAll()
        {
            return context.Products
                .Include(e => e.Brands)
                .Include(c => c.Category)
                .Include(v => v.Discount)
                .Include(i=>i.ProductImages)
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
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public List<Product> Filter(string? brandName = null, decimal? minPrice = null, decimal? maxPrice = null, int? rating = null)
        {
            var query = context.Products.Include(e=>e.Brands).AsQueryable();

            // Brand Filter
            if (!string.IsNullOrEmpty(brandName))
            {
                query = query.Where(e => e.Brands.Name == brandName);
            }

            // Price Filter
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                query = query.Where(e => e.Price >= minPrice.Value && e.Price <= maxPrice.Value);
            }
            else if (minPrice.HasValue)
            {
                query = query.Where(e => e.Price >= minPrice.Value);
            }
            else if (maxPrice.HasValue)
            {
                query = query.Where(e => e.Price <= maxPrice.Value);
            }

            // Rating Filter
            if (rating.HasValue)
            {
                //if (rating > 5 || rating <= 0)
                //{
                //    throw new Exception("Rating should be between 1 and 5");
                //}
                query = query.Where(e => e.AverageRating >= rating && e.AverageRating < rating + 1);
            }

            // Execute the query and convert the results to a list of Products
            List<Product> filterResults = query.Include(e => e.ProductImages).Include(e=>e.Brands).Include(e => e.Discount).Select(item => new Product
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                QuantityInStock = item.QuantityInStock,
                BrandId = item.BrandId,
                DiscountId = item.DiscountId,
                Discount = item.Discount,
                AverageRating = item.AverageRating,
                ProductImages = item.ProductImages
            }).ToList();

            return filterResults;
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
