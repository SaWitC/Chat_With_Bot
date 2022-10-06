using System.ComponentModel.DataAnnotations;

namespace FileServer.Domain.Models.File
{
    public class FileModel
    {
        //public string Id { get; set; }
        [Key]
        public string FileTitle { get; set; }
        public string UserId { get; set; }
        public string BlobName { get; set; }
        public DateTime Created { get; set; }

    }
}
