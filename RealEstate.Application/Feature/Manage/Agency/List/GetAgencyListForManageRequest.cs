using RealEstate.Common.Enums.Agency;

namespace RealEstate.Application.Feature.Manage.Agency.List
{
    public class GetAgencyListForManageRequest : IRequest<GetAgencyListForManageResponse>
    {
        public int? AgencyId { get; set; }
        public AgencyType? AgencyType { get; set; }
        public string? Name { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? UpdateDateFrom { get; set; }
        public DateTime? UpdateDateTo { get; set; }
        public DateTime? DeleteDateFrom { get; set; }
        public DateTime? DeleteDateTo { get; set; }
        public int? PageSize { get; set; } = 20;
        public int? Page { get; set; } = 1;
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
