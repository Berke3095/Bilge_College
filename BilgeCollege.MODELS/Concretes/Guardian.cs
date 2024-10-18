using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;

namespace BilgeCollege.MODELS.Concretes
{
    public class Guardian : BaseEntity, I_UserInterface
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string TCK { get; set; }

        // Relations
        public List<Student> Students { get; set; }
        
    }
}
