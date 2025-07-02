using RealEstate.Common.Enums.Apartment;

namespace RealEstate_Manage.Helpers
{
    public static class ApartmentStatusHelper
    {
        public static string GetGeorgianStatus(ApartmentStatus? status)
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
    }
}