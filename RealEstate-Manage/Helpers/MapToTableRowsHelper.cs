using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Models.User.List;

namespace RealEstate_Manage.Helpers
{
    public class MapToTableRowsHelper
    {
        public List<UserTableRowViewModel> MapToTableRows(List<GetUserListForManageItemsResponse> apiResponse)
        {
            return apiResponse.Select(user => new UserTableRowViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName ?? null,
                LastName = user.LastName ?? null,
                PIN = user.PIN,
                UserName = user.UserName ?? null,
                Type = user.Type ?? null,
                Email = user.Email ?? null,
                PhoneNumber = user.PhoneNumber ?? null,
                Balance = user.Balance ?? null,
                IsBlocked = user.IsBlocked,
                BlockDate = user.BlockDate ?? null,
                Gender = user.Gender ?? null
            }).ToList();
        }
    }
}
