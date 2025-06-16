using RealEstate.Common.Enums.Apartment;

namespace RealEstate.Application.Feature.Profile.Apartments
{
    public class MyApartmentsRequest : IRequest<MyApartmentsResponse>
    {
        public string? UserId { get; set; }

        public ApartmentStatus? Status { get; set; }
    }
}
