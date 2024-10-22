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
    }
}
