using RealEstate.Application.Feature.Manage.Apartment.List;

namespace RealEstate.Application.Feature.Manage.Export.Apartment
{
    public class ExportApartmentListCsvRequest : IRequest<ExportApartmentListCsvResponse>
    {
        public GetApartmentListForManageRequest Filter { get; set; } = new();
    }
}
