using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class SearchRepository : ISearchRepository
    {
        ApplicationDbContext context;
        public SearchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Product> SearchProducts(string search)
        {
            var searchResults = context.Products.Include(e => e.Brands).Include(e => e.Discount).Include(e => e.ProductImages).Where(e => e.Name.Contains(search)).ToList();
            return searchResults;
        }
    }
}
