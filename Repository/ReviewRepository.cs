using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_Commerce_GP.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.Include(r => r.ApplicationUser).ToList();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.Include(r => r.ApplicationUser).FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews.Include(r => r.ApplicationUser).Where(r => r.ProductId == productId).ToList();
        }

        public void Create(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var review = GetById(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}
