using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    internal class DaySchedule_AltTopicConfig : IEntityTypeConfiguration<DaySchedule_AltTopic>
    {
        public void Configure(EntityTypeBuilder<DaySchedule_AltTopic> builder)
        {
            builder.HasKey(x => new { x.DayScheduleId, x.AltTopicId });

            builder
                .Ignore(x => x.Id)
                .Ignore(x => x.GuidId)
                .Ignore(x => x.CreatedDate)
                .Ignore(x => x.ModifiedDate)
                .Ignore(x => x.State);
        }
    }
}
