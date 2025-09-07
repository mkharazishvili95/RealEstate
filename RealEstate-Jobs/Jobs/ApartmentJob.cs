using RealEstate_Jobs.Services;

namespace RealEstate_Jobs.Jobs
{
    public class ApartmentJob
    {
        private readonly IApartmentJobService _apartmentJobService;

        public ApartmentJob(IApartmentJobService apartmentJobService)
        {
            _apartmentJobService = apartmentJobService;
        }

        public async Task Execute()
        {
            await _apartmentJobService.ExpireApartments();
        }
    }
}
