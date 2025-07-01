using RealEstate.Common.Enums.Agency;

namespace RealEstate_Manage.Models.Agency
{
    public class GetMyAgenciesRequestModel
    {
        public string? UserId { get; set; }
        public string? AgencyName { get; set; }
        public string? IdentificationNumber { get; set; }
        public AgencyType? Type { get; set; }
    }
}
