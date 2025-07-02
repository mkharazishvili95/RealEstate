using RealEstate.Common.Enums.Apartment;

namespace RealEstate_Manage.Models.Profile
{
    public class MyApartmentsRequestModel
    {
        public string? UserId { get; set; }
        public ApartmentStatus? Status { get; set; }
    }
}
