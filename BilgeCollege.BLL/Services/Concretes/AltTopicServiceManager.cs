using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class AltTopicServiceManager : BaseServiceManager<AltTopic>, I_AltTopicServiceManager
    {
        public AltTopicServiceManager(I_Repository<AltTopic> repository) : base(repository)
        {

        }
    }
}
