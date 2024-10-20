using BilgeCollege.MODELS.Concretes.CustomUser;

namespace BilgeCollege.BLL.Utils
{
    public interface I_UserIntegrateServiceManager
    {
        public Task<User> CreateUserAsync(string firstName, string lastName, string tck);
    }
}
