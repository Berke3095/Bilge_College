using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class AltTopicConfig : IEntityTypeConfiguration<AltTopic>
    {
        public void Configure(EntityTypeBuilder<AltTopic> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TopicCode).HasMaxLength(20);
        }
    }
}
