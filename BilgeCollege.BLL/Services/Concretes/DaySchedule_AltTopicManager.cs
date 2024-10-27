using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class DaySchedule_AltTopicManager : BaseServiceManager<DaySchedule_AltTopic>, I_DaySchedule_AltTopicManager
    {
        public DaySchedule_AltTopicManager(I_Repository<DaySchedule_AltTopic> repository) : base(repository)
        {

        }
    }
}
