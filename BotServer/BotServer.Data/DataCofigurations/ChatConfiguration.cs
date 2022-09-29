using BotServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotServer.Data.DataCofigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatModel>
    {
        public void Configure(EntityTypeBuilder<ChatModel> builder)
        {
            builder.Property(x => x.avtorId).IsRequired();

            builder.Property(x => x.Created).IsRequired();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(25);

            builder.HasKey(x => x.id);
        }
    }
}
