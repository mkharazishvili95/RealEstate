namespace RealEstate.Application.Models.Apartment
{
    public class BlockApartmentResponse : ResponseBaseModel { }
    public class BlockApartmentRequest : IRequest<BlockApartmentResponse>
    {
        public int? ApartmentId { get; set; }
        public string? BlockReason { get; set; }
    }
}
