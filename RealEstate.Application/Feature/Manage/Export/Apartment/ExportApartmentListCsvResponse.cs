namespace RealEstate.Application.Feature.Manage.Export.Apartment
{
    public class ExportApartmentListCsvResponse
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; } = "apartments.csv";
        public string ContentType { get; set; } = "text/csv";
    }
}
