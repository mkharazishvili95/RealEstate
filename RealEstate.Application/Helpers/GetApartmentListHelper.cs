using System.Linq.Expressions;

namespace RealEstate.Application.Feature.Manage.Apartment.List
{
    public static class GetApartmentListForManageHelper
    {
        public static Expression<Func<RealEstate.Core.Apartment.Apartment, bool>> WhereClause(this GetApartmentListForManageRequest request)
        {
            return apartment =>
                (!request.ApartmentId.HasValue || apartment.ApartmentId == request.ApartmentId) &&
                (string.IsNullOrEmpty(request.Title) || apartment.Title.ToUpper().Contains(request.Title.ToUpper())) &&
                (!request.Status.HasValue || apartment.Status == request.Status) &&
                (!request.CreateDateFrom.HasValue || apartment.CreateDate >= request.CreateDateFrom) &&
                (!request.CreateDateTo.HasValue || apartment.CreateDate <= request.CreateDateTo) &&
                (!request.UpdateDateFrom.HasValue || apartment.UpdateDate >= request.UpdateDateFrom) &&
                (!request.UpdateDateTo.HasValue || apartment.UpdateDate <= request.UpdateDateTo) &&
                (!request.DeleteDateFrom.HasValue || apartment.DeleteDate >= request.DeleteDateFrom) &&
                (!request.DeleteDateTo.HasValue || apartment.DeleteDate <= request.DeleteDateTo) &&
                (string.IsNullOrEmpty(request.UserId) || apartment.UserId == request.UserId) &&
                (string.IsNullOrEmpty(request.UserPin) || apartment.User.PIN.Contains(request.UserPin)) &&
                (!request.PriceFrom.HasValue || apartment.Price >= request.PriceFrom) &&
                (!request.PriceTo.HasValue || apartment.Price <= request.PriceTo) &&
                (!request.CurrencyId.HasValue || apartment.CurrencyId == request.CurrencyId) &&
                (!request.AgencyId.HasValue || apartment.AgencyId == request.AgencyId);
        }
    }
}
