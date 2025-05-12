namespace RealEstate.Application.Models.Apartment
{
    public class UnblockApartmentResponse : ResponseBaseModel{ }
    public class UnblockApartmentRequest : IRequest<UnblockApartmentResponse>
    {
        public int? ApartmentId { get; set; }
    }
}
