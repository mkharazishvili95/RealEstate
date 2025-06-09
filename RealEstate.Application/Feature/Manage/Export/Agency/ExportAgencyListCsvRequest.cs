using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Manage.Agency.List;

namespace RealEstate.Application.Feature.Manage.Export.Agency
{
    public class ExportAgencyListCsvRequest : IRequest<ExportAgencyListCsvResponse>
    {
        public GetAgencyListForManageRequest Filter { get; set; } = new();
    }
}
