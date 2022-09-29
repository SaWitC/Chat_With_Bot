using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotServer.Data.DataCofigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageModel>
    {
        public void Configure(EntityTypeBuilder<MessageModel> builder)
        {
            builder.HasKey(x => x.id);

            builder.Property(x => x.avtroId).IsRequired();
            builder.Property(x => x.ParentId).IsRequired();

        }
    }
}
