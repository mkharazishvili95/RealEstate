namespace RealEstate.Application.Services
{
    public interface IFileStorageService
    {
        string UploadFolder { get; }
    }

    public class FileStorageService : IFileStorageService
    {
        public string UploadFolder { get; }

        public FileStorageService(string uploadFolder)
        {
            UploadFolder = uploadFolder;
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
        }
    }
}
