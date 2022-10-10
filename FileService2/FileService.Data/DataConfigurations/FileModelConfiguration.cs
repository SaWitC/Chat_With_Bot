using FileServer.Domain.Models.File;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Data.DataConfigurations
{
    public class FileModelConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(x => x.FileTitle);
            builder.Property(x => x.FileTitle).IsRequired();

            builder.Property(x => x.FileType).IsRequired();
            builder.Property(x => x.BlobName).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            
        }
    }
}
