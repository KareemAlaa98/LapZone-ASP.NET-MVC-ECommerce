using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public enum PaymentStatus
    {
        Successfull,
        Failed
    }
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public string Provider { get; set; }
        public string TransactionId { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;
    }
    
}
