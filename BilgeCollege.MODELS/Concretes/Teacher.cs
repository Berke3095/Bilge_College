using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Teacher : BaseEntity, I_UserInterface
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TCK { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }

        // Relations
        public MainTopic? MainTopic { get; set; }
        public int? MainTopicId { get; set; }

        public List<AltTopic>? AltTopics { get; set; }   
    }
}
