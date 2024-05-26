using E_Commerce_GP.Models;
using System.Collections.Generic;

namespace E_Commerce_GP.IRepository
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAll();
        Review GetById(int id);
        IEnumerable<Review> GetReviewsByProductId(int productId);
        void Create(Review review);
        void Update(Review review);
        void Delete(int id);
    }
}
