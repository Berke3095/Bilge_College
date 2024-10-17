using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;

namespace BilgeCollege.MODELS.Concretes
{
    public class Guardian : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }

        // Relations
        public List<Student> Students { get; set; }
    }
}
