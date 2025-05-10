using RealEstate.Common.Enums.Agency;

namespace RealEstate_Manage.Helpers
{
    public static class AgencyTypeHelper
    {
        public static Dictionary<AgencyType, string> GetGeorgianNames()
        {
            return new Dictionary<AgencyType, string>
            {
                { AgencyType.NotSpecified, "უცნობია" },
                { AgencyType.Agency, "სააგენტო" },
                { AgencyType.Construction, "სამშენებლო კომპანია" }
            };
        }
    }
}
