using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class AltTopicServiceManager : BaseServiceManager<AltTopic>, I_AltTopicServiceManager
    {
        private readonly I_Repository<AltTopic> _repository;

        public AltTopicServiceManager(I_Repository<AltTopic> repository) : base(repository)
        {
            _repository = repository;
        }

        public string CreateTopicCode(MainTopic mainTopic)
        {
            int i = 1;

            string possibleName = mainTopic.TopicName + "_" + i;

            var altTopics = _repository.GetAll().ToList();

            if(altTopics.Where(x => x.TopicCode == possibleName).Count() == 0) return possibleName;
            else
            {
                while(true)
                {
                    i++;
                    possibleName = mainTopic.TopicName + "_" + i;

                    if (altTopics.Where(x => x.TopicCode == possibleName).Count() == 0) return possibleName;
                    else continue;
                }
            }
        }

        public void HandleOnDelete(int id, I_ClassHourServiceManager classHourServiceManager)
        {
            var altTopic = _repository.GetById(id);
            altTopic.TeacherId = null;

            var classHours = classHourServiceManager.GetAll().Where(x => x.AltTopicId == id).ToList();
            foreach (var item in classHours)
            {
                item.AltTopicId = 1; // NONE
            }
        }

        public void HandleOnDeleteAll(I_ClassHourServiceManager classHourServiceManager)
        {
            var altTopics = _repository.GetAllActives();
            foreach (var item in altTopics)
            {
                item.TeacherId = null;
            }

            var classHours = classHourServiceManager.GetAll().Where(x => x.AltTopicId != 1).ToList(); // Get all NONEs
            foreach (var item in classHours)
            {
                item.AltTopicId = 1; // NONE
            }
        }
    }
}
