using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_GP.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IEmailSender emailSender;
        IOrderRepository orderRepository;
        IShoppingCartRepository shoppingCartRepository;
        public OrderController(IOrderRepository _orderRepository, IShoppingCartRepository _shoppingCartRepository, IEmailSender emailSender) 
        { 
            this.orderRepository = _orderRepository;
            this.shoppingCartRepository = _shoppingCartRepository;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allUserOrders = orderRepository.GetAllOrdersOfUser(userId);

            return View(allUserOrders);
        }

        public IActionResult ConfirmOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order userOrder = orderRepository.CreateOrder(userId);
            string userEmail = orderRepository.GetUserEmail(userId);
            var orderItems = orderRepository.GetOrderItems(userOrder);

            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>Order Confirmation</h1>");
            sb.Append("<p>Thank you for your purchase. Your order has been confirmed with the following details:</p>");
            sb.Append($"<p>Order Code: {userOrder.OrderCode}</p>");
            sb.Append("<h2>Order Items:</h2>");
            sb.Append("<ul>");
            foreach (var item in orderItems)
            {
                decimal newPrice = 0;
                if (item.Product.DiscountId != null)
                {
                    newPrice = item.Product.Price - (item.Product.Price * item.Product.Discount.DiscountPercent / 100);
                }
                else { newPrice = item.Product.Price; }

                sb.Append($"<li>{item.Product.Name} <ul> <li>Quantity: {item.Quantity}</li>  <li>Price: {newPrice} <small>EGP</small> </li> <li>Total: {newPrice * item.Quantity} <small>EGP</small> </li> </ul> </li>");
            }
            sb.Append("</ul>");
            sb.Append($"<p>Total Amount: {userOrder.Total} <small>EGP</small> </p>");


            emailSender.SendEmailAsync(userEmail, "Order Confirmation", sb.ToString());

            return View(userOrder);
        }

        public IActionResult CancelOrder(int id)
        {
            orderRepository.CancelOrder(id);

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("OrdersPanel");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult OrdersPanel() 
        {
            var orders = orderRepository.GetAllOrders();
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var order = orderRepository.GetOrderById(id);
            return View(order);
        }
    }
}
