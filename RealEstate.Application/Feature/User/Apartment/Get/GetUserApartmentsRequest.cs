using RealEstate.Common.Models;

namespace RealEstate.Application.Feature.User.Apartment.Get
{
    public class GetUserApartmentsRequest : IRequest<GetUserApartmentsResponse>
    {
        public string UserId { get; set; }
        public Pagination? Pagination { get; set; }
    }
}
