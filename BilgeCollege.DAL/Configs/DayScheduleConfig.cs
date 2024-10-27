using BilgeCollege.MODELS.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class DayScheduleConfig : IEntityTypeConfiguration<DaySchedule>
    {
        public void Configure(EntityTypeBuilder<DaySchedule> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
