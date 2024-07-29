using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Areas.Admin.Models.Rental
{
    public class RentalListVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public EntityLayer.Entities.Customer Customer { get; set; }
        [Required]
        public EntityLayer.Entities.Car Car { get; set; }
        [Required]
        public DateTime StartingDate { get; set; }
        [Required]
        public DateTime EndingDate { get; set; }
        [Required]
        public int Payment { get; set; }
    }
}
