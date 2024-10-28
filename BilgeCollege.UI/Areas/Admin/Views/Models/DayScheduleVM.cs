using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class DayScheduleVM
    {
        public string GuidId { get; set; }
        public DaySchedule DaySchedule { get; set; }
        public AltTopic[] AltTopics = new AltTopic[8];
    }
}
