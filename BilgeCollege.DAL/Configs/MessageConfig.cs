using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Sender)
                .WithMany(x => x.SentMessages)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedMessages)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
