namespace RealEstate.Application.Models.Agency
{
    public class AgencyApproveResponseModel : ResponseBaseModel { }

    public class AgencyApproveRequest : IRequest<AgencyApproveResponseModel>
    {
        public int AgencyId { get; set; }
    }
}
