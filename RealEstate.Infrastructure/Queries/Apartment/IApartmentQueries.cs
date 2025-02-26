using RealEstate.Infrastructure.Queries.Models.Apartment;

namespace RealEstate.Infrastructure.Queries.Apartment
{
    public interface IApartmentQueries
    {
        Task<GetApartmentDetailsModel> GetApartment(int apartmentId);
        Task<GetFiltredApartmentsModel> GetFiltredApartments();
        Task<GetNewApartmentsModel> GetNewApartments();
    }
}
