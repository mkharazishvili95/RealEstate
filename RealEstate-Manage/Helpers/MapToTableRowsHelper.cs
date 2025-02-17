using RealEstate.Application.Feature.Manage.Agency.List;
using RealEstate.Application.Feature.Manage.User.List;
using RealEstate.Models.User.List;
using RealEstate_Manage.Models.Agency.List;

namespace RealEstate_Manage.Helpers
{
    public class MapToTableRowsHelper
    {
        public List<UserTableRowViewModel> MapToTableRowsForUsers(List<GetUserListForManageItemsResponse> apiResponse)
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
                Gender = user.Gender ?? null,
                RegisterDate = user.RegisterDate ?? null
            }).ToList();
        }
        public List<AgencyTableRowViewModel> MapToTableRowsForAgency(List<GetAgencyListForManageItemsResponse> apiResponse)
        {
            return apiResponse.Select(agency => new AgencyTableRowViewModel
            {
                 IdentificationNumber = agency.IdentificationNumber ?? null,
                 PhoneNumber = agency.PhoneNumber ?? null,
                 Name = agency.Name ?? null,
                 AgencyId = agency.AgencyId,
                 AgencyType = agency.AgencyType ?? null,
                 CreateDate = agency.CreateDate ?? null,
                 DeleteDate = agency.DeleteDate ?? null,
                 Email = agency.Email ?? null,
                 IsApproved = agency.IsApproved ?? null,
                 IsDeleted = agency.IsDeleted ?? null,
                 UpdateDate = agency.UpdateDate ?? null,
                 UserId = agency.UserId ?? null,
                 UserPin = agency.UserPin ?? null

            }).ToList();
        }
    }
}
