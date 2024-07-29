using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Areas.Admin.Models.Rental
{
    public class RentalCreateVM
    {
        [Required]
        [DisplayName("Select Customer: ")]
        public Guid CustomerId { get; set; }
        [Required]
        [DisplayName("Select Car: ")]
        public Guid CarId { get; set; }

        [Required]
        [DisplayName("Starting Date: ")]
        public DateTime StartingDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Ending Date: ")]
        public DateTime EndingDate { get; set; } = DateTime.Now.AddDays(1);

        public CustomerCarVM CustomerCar { get; set; } = new CustomerCarVM();
    }
}
