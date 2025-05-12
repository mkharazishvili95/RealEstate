namespace RealEstate.Application.Feature.Manage.Apartment.Block
{
    public class BlockApartmentRequest : IRequest<BlockApartmentResponse>
    {
        public int? ApartmentId { get; set; }
        public string? BlockReason { get; set; }
    }
}
