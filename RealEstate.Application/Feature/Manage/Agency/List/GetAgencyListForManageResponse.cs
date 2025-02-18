using RealEstate.Application.Models;
using RealEstate.Common.Enums.Agency;

namespace RealEstate.Application.Feature.Manage.Agency.List
{
    public class GetAgencyListForManageResponse : ResponseBaseModel
    {
        public List<GetAgencyListForManageItemsResponse> AgencyListForManage { get; set; } = new();
        //public Pagination? Pagination { get; set; }
        public int TotalCount { get; set; }
    }
    public class GetAgencyListForManageItemsResponse
    {
        public int AgencyId { get; set; }
        public AgencyType? AgencyType { get; set; }
        public string? Name { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? UserId { get; set; }
        public string? UserPin { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
