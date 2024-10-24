using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class GuardianServiceManager : BaseServiceManager<Guardian>, I_GuardianServiceManager
    {
        private readonly I_Repository<Guardian> _repository;

        public GuardianServiceManager(I_Repository<Guardian> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateUserAsync(UserManager<User> _userManager, string firstName, string lastName, string tck)
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

        public async Task HandleOnDestroy(UserManager<User> userManager, Guardian guardianToDestroy)
        {
            var guardian = await userManager.FindByIdAsync(guardianToDestroy.UserId);
            if (guardian != null) await userManager.DeleteAsync(guardian);
        }

        public async Task<Guardian> SetupGuardian(User user, UserManager<User> _userManager, string firstName, string lastName, string tck, string phoneNumber, string homeAddress)
        {
            user.PhoneNumber = phoneNumber;
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                string role = "Guardian";
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (roleResult.Succeeded)
                {
                    Guardian guardian = new Guardian
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = user.Email,
                        TCK = tck,
                        PhoneNumber = phoneNumber,
                        HomeAddress = homeAddress,
                        UserId = user.Id
                    };

                    return guardian;
                }
            }
            return null;
        }
    }
}
