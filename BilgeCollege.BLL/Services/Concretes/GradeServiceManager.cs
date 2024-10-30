using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class GradeServiceManager : BaseServiceManager<Grade>, I_GradeServiceManager
    {
        public GradeServiceManager(I_Repository<Grade> repository) : base(repository)
        {

        }
    }
}
