using RealEstate.Common.Enums.Agency;

namespace RealEstate.Core.Agency
{
    public class Agency
    {
        public int AgencyId { get; set; }
        public string UserId { get; set; }
        public AgencyType AgencyType { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleteReason { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? Address { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Link { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
