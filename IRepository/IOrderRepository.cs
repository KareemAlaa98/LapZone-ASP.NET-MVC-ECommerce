using E_Commerce_GP.Models;

namespace E_Commerce_GP.IRepository
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersOfUser(string userId);
        string GenerateOrderCode();
        Order CreateOrder(string userId);
        public string GetUserEmail(string userId);

        public List<OrderItem> GetOrderItems(Order order);

        public Order GetOrderById(int id);
        public void CancelOrder(int id);


    }
}
