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
