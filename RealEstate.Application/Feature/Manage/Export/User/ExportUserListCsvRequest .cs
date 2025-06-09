using RealEstate.Application.Feature.Manage.User.List;

namespace RealEstate.Application.Feature.Manage.Export.User
{
    public class ExportUserListCsvRequest : IRequest<ExportUserListCsvResponse>
    {
        public GetUserListForManageRequest Filter { get; set; } = new();
    }
}
