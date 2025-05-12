using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Common.Enums.Agency;
using RealEstate.Common.Enums.Apartment;

namespace RealEstate_Manage.Extensions
{
    public static class AgencyTypeExtensions
    {
        public static string ToGeorgian(this AgencyType agencyType)
        {
            switch (agencyType)
            {
                case AgencyType.NotSpecified:
                    return "უცნობია";
                case AgencyType.Agency:
                    return "სააგენტო";
                case AgencyType.Construction:
                    return "სამშენებლო კომპანია";
                default:
                    return "უცნობია";
            }
        }
        public static string GetGeorgianStatus(ApartmentStatus status)
        {
            return status switch
            {
                ApartmentStatus.Active => "აქტიური",
                ApartmentStatus.Expired => "ვადაგასული",
                ApartmentStatus.Blocked => "დაბლოკილი",
                ApartmentStatus.Deleted => "წაშლილი",
                _ => ""
            };
        }
        public static List<SelectListItem> GetAgencyTypeOptions()
        {
            var agencyTypes = Enum.GetValues(typeof(AgencyType))
                                  .Cast<AgencyType>()
                                  .Select(at => new SelectListItem
                                  {
                                      Text = at.ToGeorgian(),
                                      Value = at.ToString()
                                  })
                                  .ToList();
            return agencyTypes;
        }
    }
}
