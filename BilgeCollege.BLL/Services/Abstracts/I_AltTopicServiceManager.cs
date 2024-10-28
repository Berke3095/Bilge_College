using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_AltTopicServiceManager : I_BaseServiceManager<AltTopic>
    {
        public string CreateTopicCode(MainTopic mainTopic);
        public void HandleOnDelete(int id, I_ClassHourServiceManager classHourServiceManager);
        public void HandleOnDeleteAll(I_ClassHourServiceManager classHourServiceManager);
    }
}
