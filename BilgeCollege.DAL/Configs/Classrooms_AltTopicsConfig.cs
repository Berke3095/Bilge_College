using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class Classrooms_AltTopicsConfig : IEntityTypeConfiguration<Classrooms_AltTopics>
    {
        public void Configure(EntityTypeBuilder<Classrooms_AltTopics> builder)
        {
            builder
                .Ignore(x => x.Id)
                .Ignore(x => x.GuidId)
                .Ignore(x => x.CreatedDate)
                .Ignore(x => x.ModifiedDate);

            builder.HasKey(x => new {x.ClassroomId, x.AltTopicId});

        }
    }
}
