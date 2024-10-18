using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class GuardianServiceManager : BaseServiceManager<Guardian>, I_GuardianServiceManager
    {
        public GuardianServiceManager(I_Repository<Guardian> repository) : base(repository)
        {

        }
    }
}
