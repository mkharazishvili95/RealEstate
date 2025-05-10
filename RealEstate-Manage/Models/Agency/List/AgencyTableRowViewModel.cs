using RealEstate.Common.Enums.Agency;

namespace RealEstate_Manage.Models.Agency.List
{
    public class AgencyTableRowViewModel
    {
        public int AgencyId { get; set; }
        public AgencyType? AgencyType { get; set; }
        public string? Name { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? UserId { get; set; }
        public string? OwnerPIN { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
