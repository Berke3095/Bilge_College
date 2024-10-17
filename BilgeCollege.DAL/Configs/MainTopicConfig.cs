using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class MainTopicConfig : IEntityTypeConfiguration<MainTopic>
    {
        public void Configure(EntityTypeBuilder<MainTopic> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TopicName).HasMaxLength(50);
        }
    }
}
