using System.Linq.Expressions;

namespace RealEstate.Application.Feature.Manage.Agency.List
{
    public static class GetAgencyListForManageHelper
    {
        public static Expression<Func<RealEstate.Core.Agency.Agency, bool>> WhereClause(this GetAgencyListForManageRequest request)
        {
            return agency =>
                (request.AgencyId == null || agency.AgencyId == request.AgencyId) &&
                (request.AgencyType == null || agency.AgencyType == request.AgencyType) &&
                (string.IsNullOrEmpty(request.Name) || agency.Name.ToUpper().Contains(request.Name.ToUpper())) &&
                (request.IsApproved == null || agency.IsApproved == request.IsApproved) &&
                (request.IsDeleted == null || agency.IsDeleted == request.IsDeleted) &&
                (string.IsNullOrEmpty(request.IdentificationNumber) || agency.IdentificationNumber.Contains(request.IdentificationNumber)) &&
                (string.IsNullOrEmpty(request.Email) || agency.Email.ToUpper().Contains(request.Email.ToUpper())) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || agency.PhoneNumber.Contains(request.PhoneNumber)) &&
                (request.UpdateDateFrom == null || agency.UpdateDate >= request.UpdateDateFrom) &&
                (request.UpdateDateTo == null || agency.UpdateDate <= request.UpdateDateTo) &&
                (request.DeleteDateFrom == null || agency.DeleteDate >= request.DeleteDateFrom) &&
                (request.DeleteDateTo == null || agency.DeleteDate <= request.DeleteDateTo) &&
                (request.CreateDateFrom == null || agency.CreateDate >= request.CreateDateFrom) &&
                (request.CreateDateTo == null || agency.CreateDate <= request.CreateDateTo);
        }
    }
}
