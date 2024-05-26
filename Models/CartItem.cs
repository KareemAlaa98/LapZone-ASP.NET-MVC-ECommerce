using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CartId is required")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }


        [Required(ErrorMessage = "ProductId is required")]
        public int ProductId { get; set; }
        public Product Product { get; set; }


        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }


        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; } = null;
    }
}
