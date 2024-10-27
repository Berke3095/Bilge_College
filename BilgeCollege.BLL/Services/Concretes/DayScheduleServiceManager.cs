using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class DayScheduleServiceManager : BaseServiceManager<DaySchedule>, I_DayScheduleServiceManager
    {
        public DayScheduleServiceManager(I_Repository<DaySchedule> repository) : base(repository)
        {

        }
    }
}
