using BilgeCollege.MODELS.Concretes;
using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Areas.Admin.Models
{
    public class MainTopicVM
    {
        [Required(ErrorMessage = "You must give it a name.")]
        public string TopicName { get; set; }

        public List<MainTopic> MainTopics { get; set; }
    }
}
