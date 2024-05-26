using System.ComponentModel.DataAnnotations;

namespace E_Commerce_GP.ViewModels
{
    public class RoleViewModel
    {
        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
