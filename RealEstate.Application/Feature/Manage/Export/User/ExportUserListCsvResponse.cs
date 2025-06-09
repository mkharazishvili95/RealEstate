namespace RealEstate.Application.Feature.Manage.Export.User
{
    public class ExportUserListCsvResponse
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; } = "users.csv";
        public string ContentType { get; set; } = "text/csv";
    }
}
