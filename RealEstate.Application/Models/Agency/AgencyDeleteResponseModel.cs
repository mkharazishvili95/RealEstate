namespace RealEstate.Application.Models.Agency
{
    public class AgencyDeleteResponseModel : ResponseBaseModel { }
    public class AgencyDeleteRequest : IRequest<AgencyDeleteResponseModel>
    {
        public int AgencyId { get; set; }
        public string? DeleteReason { get; set; }
    }
}
