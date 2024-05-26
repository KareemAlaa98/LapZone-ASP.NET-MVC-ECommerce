using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Repository
{
    public class OrderRepository : IOrderRepository
    {
        ApplicationDbContext context;
        IShoppingCartRepository shoppingCartRepository;

        public OrderRepository(ApplicationDbContext _context, IShoppingCartRepository _shoppingCartRepository)
        {
            this.context = _context;
            this.shoppingCartRepository = _shoppingCartRepository;
        }

        public List<Order> GetAllOrders()
        {
            return context.Orders.Include(e=>e.ApplicationUser).ToList();
        }

        public string GetUserEmail(string userId)
        {
            return context.Users.FirstOrDefault(e => e.Id == userId).Email;
        }

        public string GenerateOrderCode()
        {
            // Generate a random 16-digit number
            string randomNumber = Guid.NewGuid().ToString("N").Substring(0, 16);

            // Format the number with hyphens after every 4 characters
            return randomNumber.Substring(0, 4) + "-" +
                   randomNumber.Substring(4, 4) + "-" +
                   randomNumber.Substring(8, 4) + "-" +
                   randomNumber.Substring(12, 4);
        }

        public Order GetOrderById(int id)
        {
            return context.Orders.Include(e => e.ApplicationUser).Include(e => e.OrderItems).ThenInclude(e => e.Product).ThenInclude(e => e.Discount).Where(e => e.Id == id).FirstOrDefault();
        }
        
        public List<Order> GetAllOrdersOfUser(string userId)
        {
            return context.Orders
                .Include(e => e.OrderItems)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Discount)
                .Include(e => e.OrderItems)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.ProductImages)
                .Where(e => e.ApplicationUserId == userId).ToList();
        }

        public Order CreateOrder(string userId)
        {
            var userShoppingCart = shoppingCartRepository.GetUserShoppingCart(userId);

            Order userOrder = new Order();
            userOrder.ApplicationUserId = userId;
            userOrder.OrderCode = GenerateOrderCode();
            userOrder.PaymentId = 3;
            userOrder.Total = userShoppingCart.Total;
            userOrder.Status = OrderStatus.Processing;
            userOrder.OrderItems = new List<OrderItem>();

            context.Orders.Add(userOrder);
            context.SaveChanges();

            foreach (var cartItem in userShoppingCart.CartItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ProductId = cartItem.ProductId;
                orderItem.OrderId = userOrder.Id;
                orderItem.Quantity = cartItem.Quantity;

                userOrder.OrderItems.Add(orderItem);

                cartItem.Product.QuantityInStock -= cartItem.Quantity;
                context.SaveChanges();
            }

            // Clear the CartItems list
            userShoppingCart.CartItems.Clear();
            userShoppingCart.Total = 0;
            context.SaveChanges();

            return userOrder;
        }

        public List<OrderItem> GetOrderItems(Order order)
        {
            return context.OrderItems.Include(e => e.Product).ThenInclude(e => e.Discount).Where(e => e.OrderId == order.Id).ToList();
        }

        public void CancelOrder(int id)
        {
            var order = GetOrderById(id);
            foreach(var item in order.OrderItems)
            {
                item.Product.QuantityInStock += item.Quantity;
                context.SaveChanges();
            }
            order.Status = OrderStatus.Canceled;
            order.ModifiedAt = DateTime.Now;
            context.SaveChanges();
        }
    }
}
