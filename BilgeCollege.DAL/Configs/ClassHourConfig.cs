using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class ClassHourConfig : IEntityTypeConfiguration<ClassHour>
    {
        public void Configure(EntityTypeBuilder<ClassHour> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.State);
        }
    }
}
