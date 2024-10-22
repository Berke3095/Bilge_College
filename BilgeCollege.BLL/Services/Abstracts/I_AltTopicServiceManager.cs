using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_AltTopicServiceManager : I_BaseServiceManager<AltTopic>
    {
        public string CreateTopicCode(MainTopic mainTopic);
    }
}
