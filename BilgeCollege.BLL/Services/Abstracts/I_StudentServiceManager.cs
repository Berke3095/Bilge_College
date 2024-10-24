using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_StudentServiceManager : I_BaseServiceManager<Student>, I_UserIntegrateServiceManager
    {
        public Task<Student> SetupStudent(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, int? GuardianId);
    }
}
