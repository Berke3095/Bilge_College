using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class MainTopicServiceManager : BaseServiceManager<MainTopic>, I_MainTopicServiceManager
    {
        private readonly I_Repository<MainTopic> _repository;

        public MainTopicServiceManager(I_Repository<MainTopic> repository) : base(repository)
        {
            _repository = repository;
        }

        public void HandleRelationOnUpdate(I_AltTopicServiceManager _altTopicServiceManager, List<AltTopic> altTopicsToUpdate, MainTopic mainTopic)
        {
            foreach (var item in altTopicsToUpdate)
            {
                item.TopicCode = _altTopicServiceManager.CreateTopicCode(mainTopic);
            }
            _altTopicServiceManager.UpdateRange(altTopicsToUpdate);
        }

        public void HandleRelationsOnDelete(I_AltTopicServiceManager _altTopicServiceManager, List<AltTopic> passiveAltTopics, int relatedId, I_TeacherServiceManager _teacherServiceManager)
        {
            var owningTeachers = _teacherServiceManager.GetAll().Where(x => x.MainTopicId == relatedId).ToList();
            if (owningTeachers.Count() > 0)
            {
                foreach (var item in owningTeachers)
                {
                    item.MainTopicId = null;
                }
            }

            var altTopics = _altTopicServiceManager.GetAllActives().Where(x => x.MainTopicId == relatedId).ToList();
            if (altTopics.Count() > 0)
            {
                _altTopicServiceManager.DeleteRange(altTopics);
            } 
        }

        public void HandleRelationsOnDestroy(I_AltTopicServiceManager _altTopicServiceManager, List<AltTopic> passiveAltTopics, int relatedId, I_TeacherServiceManager _teacherServiceManager)
        {
            var owningTeachers = _teacherServiceManager.GetAll().Where(x => x.MainTopicId == relatedId).ToList();
            if (owningTeachers.Count() > 0)
            {
                foreach (var item in owningTeachers)
                {
                    item.MainTopicId = null;
                }
            }

            var altTopics = passiveAltTopics.Where(x => x.MainTopicId == relatedId).ToList();
            if (altTopics.Count() > 0)
            {
                _altTopicServiceManager.DestroyRange(altTopics);
            }
        }

        public MainTopic SetMainTopic(string topicName)
        {
            MainTopic mainTopic = new MainTopic();

            if (_repository.GetAll().Where(x => x.TopicName == topicName).ToList().Count() == 0) mainTopic.TopicName = topicName;
            else
            {
                int i = 1;
                while (true)
                {
                    string possibleName = topicName + i;
                    if (_repository.GetAll().Where(x => x.TopicName == possibleName).ToList().Count() == 0)
                    {
                        mainTopic.TopicName = possibleName;
                        break;
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }
            }

            return mainTopic;
        }
    }
}
