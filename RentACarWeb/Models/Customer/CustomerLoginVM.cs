using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Models.Customer
{
    public class CustomerLoginVM
    {
        [Required]
        [DisplayName("Username:")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
