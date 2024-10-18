using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class MainTopicServiceManager : BaseServiceManager<MainTopic>, I_MainTopicServiceManager
    {
        public MainTopicServiceManager(I_Repository<MainTopic> repository) : base(repository)
        {

        }
    }
}
