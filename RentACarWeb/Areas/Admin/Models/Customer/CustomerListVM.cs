using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Areas.Admin.Models.Customer
{
    public class CustomerListVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public int Budget { get; set; }
        public bool isActive { get; set; }
    }
}
