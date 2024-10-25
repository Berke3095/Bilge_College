using BilgeCollege.BLL.Utils;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.MODELS.Enums;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_StudentServiceManager : I_BaseServiceManager<Student>
    {
        public Task<Student> SetupStudent(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, GenderEnum gender, string finishedSchool, double finalGrade, int? classroomId, int? guardianId);
    }
}
