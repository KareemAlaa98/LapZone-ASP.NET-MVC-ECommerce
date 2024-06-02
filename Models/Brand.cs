namespace E_Commerce_GP.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
