using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;

namespace BilgeCollege.MODELS.Concretes
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Relations
        public MainTopic MainTopic { get; set; }
        public int MainTopicId { get; set; }
    }
}
