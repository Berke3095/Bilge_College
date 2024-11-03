using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class MessageServiceManager : BaseServiceManager<Message>, I_MessageServiceManager
    {
        public MessageServiceManager(I_Repository<Message> repository) : base(repository)
        {

        }
    }
}
