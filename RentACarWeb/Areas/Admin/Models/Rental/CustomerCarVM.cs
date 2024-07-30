namespace RentACarWeb.Areas.Admin.Models.Rental
{
    public class CustomerCarVM
    {
        public List<EntityLayer.Entities.Customer>? Customers { get; set; }
        public List<EntityLayer.Entities.Car>? Cars { get; set; }
    }
}
