using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Areas.Admin.Models.Customer
{
    public class CustomerCreateVM
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid phone number.")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public int Budget { get; set; }
    }
}
