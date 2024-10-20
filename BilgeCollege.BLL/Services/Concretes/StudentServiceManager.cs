using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class StudentServiceManager : BaseServiceManager<Student>, I_StudentServiceManager
    {
        public StudentServiceManager(I_Repository<Student> repository) : base(repository)
        {

        }
    }
}
