using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_TeacherServiceManager : I_BaseServiceManager<Teacher>
    {
        public Task<Teacher> SetupTeacher(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, string phoneNumber, int? mainTopicId);
        public Task HandleOnDestroy(UserManager<User> userManager, Teacher teacherToDestroy);
    }
}
