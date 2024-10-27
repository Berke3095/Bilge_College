using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class Classrooms_AltTopicsManager : BaseServiceManager<Classrooms_AltTopics>, I_Classrooms_AltTopicsManager
    {
        public Classrooms_AltTopicsManager(I_Repository<Classrooms_AltTopics> repository) : base(repository)
        {

        }
    }
}
