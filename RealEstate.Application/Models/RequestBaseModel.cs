namespace RealEstate.Application.Models
{
    public abstract class RequestBaseModel
    {
        public string? Key { get; set; }

        public string? RequestType { get; set; }

        public abstract (string key, string requestType) GetBaseRequestInfo();
    }
}
