namespace RealEstate.Application.Services
{
    public interface IAgencyService
    {
        Task<Core.Agency.Agency> GetAgencyById(int id);
    }
}
