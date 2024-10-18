using BilgeCollege.MODELS.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.MODELS.Concretes.CustomUser
{
    public class User : IdentityUser, I_IdentityBase
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }

        public int CustomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
