using System.Linq.Expressions;

namespace RealEstate.Application.Feature.Manage.User.List
{
    public static class GetUserListForManageHelper
    {
        public static Expression<Func<RealEstate.Core.User.User, bool>> WhereClause(this GetUserListForManageRequest request)
        {
            return user =>
                (string.IsNullOrEmpty(request.UserId) || user.UserId == request.UserId) &&
                (string.IsNullOrEmpty(request.FirstName) || user.FirstName.ToUpper().Contains(request.FirstName.ToUpper())) &&
                (string.IsNullOrEmpty(request.PIN) || user.PIN.Contains(request.PIN)) &&
                (string.IsNullOrEmpty(request.LastName) || user.LastName.ToUpper().Contains(request.LastName.ToUpper())) &&
                (string.IsNullOrEmpty(request.UserName) || user.UserName.ToUpper().Contains(request.UserName.ToUpper())) &&
                (string.IsNullOrEmpty(request.Email) || user.Email.ToUpper().Contains(request.Email.ToUpper())) &&
                (!request.Type.HasValue || user.Type == request.Type) &&
                (!request.IsBlocked.HasValue || user.IsBlocked == request.IsBlocked) &&
                (!request.BlockDateFrom.HasValue || user.BlockDate >= request.BlockDateFrom) &&
                (!request.BlockDateTo.HasValue || user.BlockDate <= request.BlockDateTo) &&
                (!request.BalanceFrom.HasValue || user.Balance >= request.BalanceFrom) &&
                (!request.BalanceTo.HasValue || user.Balance <= request.BalanceTo) &&
                (!request.Gender.HasValue || user.Gender == request.Gender) &&
                (!request.RegisterDateFrom.HasValue || user.RegisterDate >= request.RegisterDateFrom) &&
                (!request.RegisterDateTo.HasValue || user.RegisterDate <= request.RegisterDateTo);
        }
    }
}
