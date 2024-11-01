using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.BLL.Utils;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.MODELS.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class StudentServiceManager : BaseServiceManager<Student>, I_StudentServiceManager
    {
        private readonly I_Repository<Student> _repository;

        public StudentServiceManager(I_Repository<Student> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Student> SetupStudent(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, GenderEnum gender, string finishedSchool, double finalGrade, int? classroomId, int? guardianId)
        {
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                string role = "Student";
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (roleResult.Succeeded)
                {
                    Student student = new Student
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = user.Email,
                        TCK = tck,
                        Gender = gender,
                        GuardianId = guardianId,
                        UserId = user.Id,
                        FinishedSchool = finishedSchool,
                        FinalGrade = finalGrade,
                        ClassroomId = classroomId,
                        SchoolNo = SchoolNoGenerator.GetSchoolNo()
                    };

                    return student;
                }
            }
            return null;
        }
    }
}
