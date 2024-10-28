using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class ClassHourServiceManager : BaseServiceManager<ClassHour>, I_ClassHourServiceManager
    {
        public ClassHourServiceManager(I_Repository<ClassHour> repository) : base(repository)
        {

        }
    }
}
