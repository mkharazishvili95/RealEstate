namespace RealEstate.Application.Models.Agency
{
    public class AgencyRestoreResponseModel : ResponseBaseModel { }
    public class AgencyRestoreRequest : IRequest<AgencyRestoreResponseModel>
    {
        public int AgencyId { get; set; }
    }
}
