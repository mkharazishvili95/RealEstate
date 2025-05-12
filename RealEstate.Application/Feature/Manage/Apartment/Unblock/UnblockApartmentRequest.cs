namespace RealEstate.Application.Feature.Manage.Apartment.Unblock
{
    public class UnblockApartmentRequest : IRequest<UnblockApartmentResponse>
    {
        public int? ApartmentId { get; set; }
    }
}
