using System.Text.Json.Serialization;

namespace RealEstate_Manage.Models.Agency
{
    public class DeleteAgencyRequestModel
    {
        public int AgencyId { get; set; }
        public string? DeleteReason { get; set; }
    }
}
