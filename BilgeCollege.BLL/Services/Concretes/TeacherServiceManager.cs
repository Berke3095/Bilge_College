using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class TeacherServiceManager : BaseServiceManager<Teacher>, I_TeacherServiceManager
    {
        private readonly I_Repository<Teacher> _repository;

        public TeacherServiceManager(I_Repository<Teacher> repository, UserManager<User> userManager) : base(repository)
        {
            _repository = repository;
        }

        public async Task HandleOnDestroy(UserManager<User> userManager, Teacher teacherToDestroy)
        {
            var teacher = await userManager.FindByIdAsync(teacherToDestroy.UserId);
            if (teacher != null) await userManager.DeleteAsync(teacher);
        }

        public async Task<Teacher> SetupTeacher(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, string phoneNumber, int? mainTopicId)
        {
            user.PhoneNumber = phoneNumber;
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                string role = "Teacher";
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (roleResult.Succeeded)
                {
                    Teacher teacher = new Teacher
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = user.Email,
                        TCK = tck,
                        PhoneNumber = phoneNumber,
                        UserId = user.Id,
                        MainTopicId = mainTopicId
                    };

                    return teacher;
                }
            }
            return null;
        }
    }
}
