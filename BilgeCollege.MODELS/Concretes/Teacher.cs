using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Relations
        List<Student> Students { get; set; }

        public MainTopic MainTopic { get; set; }
        public int MainTopicId { get; set; }
    }
}
