using System.ComponentModel.DataAnnotations;

namespace RentACarWeb.Areas.Admin.Models.Car
{
    public class CarListVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Make { get; set; }

        [Required]

        public string Model { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]

        public int Year { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
