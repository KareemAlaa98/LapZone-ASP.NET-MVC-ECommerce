using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;

namespace E_Commerce_GP.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        ApplicationDbContext context;
        public ProductImageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(int productId, string fileName)
        {
            ProductImage productImage = new ProductImage
            {
                ImageUrl = fileName,
                ProductId = productId
            };
            context.ProdcutImages.Add(productImage);
            context.SaveChanges();
        }
        public ProductImage Get(int imageId)
        {
            return context.ProdcutImages.FirstOrDefault(image => image.Id == imageId);
        }

        public void Delete(int imageId)
        {
            var imageToDelete = Get(imageId);
            if (imageToDelete != null)
            {
                context.ProdcutImages.Remove(imageToDelete);
                context.SaveChanges();
            }
        }
        public List<ProductImage> GetProductImagesByProductId(int productId)
        {
            return context.ProdcutImages.Where(image => image.ProductId == productId).ToList();
        }
    }
}
