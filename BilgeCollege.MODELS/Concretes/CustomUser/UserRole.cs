using BilgeCollege.MODELS.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.MODELS.Concretes.CustomUser
{
    public class UserRole : IdentityRole, I_IdentityBase
    {
        public UserRole()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
