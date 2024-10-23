using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Utils
{
    public interface I_UserIntegrateServiceManager
    {
        public Task<User> CreateUserAsync(UserManager<User> _userManager, string firstName, string lastName, string tck);
    }
}
