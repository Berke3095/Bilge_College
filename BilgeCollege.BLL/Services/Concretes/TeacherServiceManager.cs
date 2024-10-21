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
        private readonly UserManager<User> _userManager;

        public TeacherServiceManager(I_Repository<Teacher> repository, UserManager<User> userManager) : base(repository)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<User> CreateUserAsync(string firstName, string lastName, string tck)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            User user = new User();

            #region FindSuitableEmail
            string bestEmail = firstName.ToLower() + "." + lastName.ToLower() + "@bilgecollege.com";
            var emailTaken = await _userManager.FindByEmailAsync(bestEmail);
            if (emailTaken == null)
            {
                user.Email = bestEmail;
            }
            else
            {
                int i = 0;
                try
                {
                    i = int.Parse(tck.Substring(0, 2));
                }
                catch (Exception)
                {
                    throw new Exception("Couldn't convert tck to int");
                }

                while (true)
                {
                    string possibleEmail = firstName.ToLower() + "." + lastName.ToLower() + i + "@bilgecollege.com";
                    emailTaken = await _userManager.FindByEmailAsync(possibleEmail);
                    if (emailTaken == null)
                    {
                        user.Email = possibleEmail;
                        break;
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }
            }
            #endregion

            #region FindSuitableUsername
            string bestUsername = firstName + "_" + lastName;
            var usernameTaken = await _userManager.FindByNameAsync(bestUsername);
            if (usernameTaken == null)
            {
                user.UserName = bestUsername;
            }
            else
            {
                int i = 0;
                try
                {
                    i = int.Parse(tck.Substring(0, 2));
                }
                catch (Exception)
                {
                    throw new Exception("Couldn't convert tck to int");
                }

                while (true)
                {
                    string possibleUsername = firstName + "_" + lastName + i;
                    usernameTaken = await _userManager.FindByNameAsync(possibleUsername);
                    if (emailTaken == null)
                    {
                        user.UserName = possibleUsername;
                        break;
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }
            } 
            #endregion

            user.PasswordHash = passwordHasher.HashPassword(user, tck);

            return user;
        }
    }
}
