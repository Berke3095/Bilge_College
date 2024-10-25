namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class AltTopicVM
    {
        public int Id { get; set; }
        public string TopicCode { get; set; }
        public int? MainTopicId { get; set; }
        public int? TeacherId { get; set; }
    }
}
