namespace RealEstate.Application.Feature.Manage.Export.Agency
{
    public class ExportAgencyListCsvResponse
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; } = "agencies.csv";
        public string ContentType { get; set; } = "text/csv";
    }
}
