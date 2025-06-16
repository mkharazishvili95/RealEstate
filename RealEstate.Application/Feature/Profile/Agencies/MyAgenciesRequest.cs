using RealEstate.Common.Enums.Agency;

namespace RealEstate.Application.Feature.Profile.Agencies
{
    public class MyAgenciesRequest : IRequest<MyAgenciesResponse>
    {
        public string? UserId { get; set; }
        public string? AgencyName { get; set; }
        public string? IdentificationNumber { get; set; }
        public AgencyType? Type { get; set; }
    }
}
