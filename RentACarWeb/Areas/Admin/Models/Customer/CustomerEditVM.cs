using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentACarWeb.Areas.Admin.Models.Customer
{
    public class CustomerEditVM
    {
        [Required]
        public Guid Id { get; set; }
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
        public bool isActive { get; set; }
    }
}
