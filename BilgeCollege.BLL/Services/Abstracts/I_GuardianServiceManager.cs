using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_GuardianServiceManager : I_BaseServiceManager<Guardian>, I_UserIntegrateServiceManager
    {
        public Task<Guardian> SetupGuardian(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, string homeAddress);
    }
}
