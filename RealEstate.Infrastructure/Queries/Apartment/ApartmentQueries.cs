using RealEstate.Infrastructure.Queries.Models.Apartment;

namespace RealEstate.Infrastructure.Queries.Apartment
{
    public class ApartmentQueries : IApartmentQueries
    {
        public Task<GetApartmentDetailsModel> GetApartment(int apartmentId)
        {
            throw new NotImplementedException();
        }

        public Task<GetFavoriteApartmentsModel> GetFavoriteApartments()
        {
            throw new NotImplementedException();
        }

        public Task<GetFiltredApartmentsModel> GetFiltredApartments()
        {
            throw new NotImplementedException();
        }

        public Task<GetNewApartmentsModel> GetNewApartments()
        {
            throw new NotImplementedException();
        }
    }
}
