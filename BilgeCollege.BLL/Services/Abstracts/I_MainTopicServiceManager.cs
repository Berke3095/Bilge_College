using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_MainTopicServiceManager : I_BaseServiceManager<MainTopic>
    {
        public MainTopic SetMainTopic(string topicName);
        public void HandleRelationsOnDestroy(I_AltTopicServiceManager _altTopicServiceManager, List<AltTopic> passiveAltTopics, int relatedId, I_TeacherServiceManager _teacherServiceManager);
        public void HandleRelationsOnDelete(I_AltTopicServiceManager _altTopicServiceManager, List<AltTopic> passiveAltTopics, int relatedId, I_TeacherServiceManager _teacherServiceManager);
    }
}
