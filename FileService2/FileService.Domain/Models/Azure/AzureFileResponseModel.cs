namespace FileServer.Domain.Models.Azure
{
    public class AzureFileResponseModel
    {
        public Stream Filestream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
