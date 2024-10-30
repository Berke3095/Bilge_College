namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class DayScheduleVM
    {
        public DayScheduleVM()
        {
            AltTopicIds = new int[8];
        }

        public int Id { get; set; }
        public string Day { get; set; }
        public int[] AltTopicIds { get; set; }
    }
}
