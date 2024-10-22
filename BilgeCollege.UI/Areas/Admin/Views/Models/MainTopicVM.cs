using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class MainTopicVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must give it a name.")]
        public string TopicName { get; set; }
    }
}
