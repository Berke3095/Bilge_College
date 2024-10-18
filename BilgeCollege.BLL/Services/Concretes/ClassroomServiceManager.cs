using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class ClassroomServiceManager : BaseServiceManager<Classroom>, I_ClassroomServiceManager
    {
        public ClassroomServiceManager(I_Repository<Classroom> repository) : base(repository)
        {

        }
    }
}
