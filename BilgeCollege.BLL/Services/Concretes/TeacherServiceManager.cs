using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class TeacherServiceManager : BaseServiceManager<Teacher>, I_TeacherServiceManager
    {
        public TeacherServiceManager(I_Repository<Teacher> repository) : base(repository)
        {

        }
    }
}
