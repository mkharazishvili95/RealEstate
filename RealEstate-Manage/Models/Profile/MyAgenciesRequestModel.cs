using RealEstate.Common.Enums.Agency;

namespace RealEstate_Manage.Models.Profile
{
    public class MyAgenciesRequestModel
    {
        public string? UserId { get; set; }
        public string? AgencyName { get; set; }
        public string? IdentificationNumber { get; set; }
        public AgencyType? Type { get; set; }
    }
}
