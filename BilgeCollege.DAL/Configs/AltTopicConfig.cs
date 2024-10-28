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

            builder.HasData(new AltTopic
            {
                Id = 1,
                MainTopicId = 1,
                TopicCode = "NONE",
                State = MODELS.Enums.StateEnum.None
            });
        }
    }
}
