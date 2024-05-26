using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_GP.Models
{
    public enum OrderStatus
    {
        Processing,
        Delivered,
        Canceled
    }
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        [ForeignKey ("ApplicationUser.Id")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [Required]
        public string OrderCode { get; set; }

        [Required]
        public OrderStatus Status { get; set; }


        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;

        public List<OrderItem> OrderItems { get; set; }
    }
}
