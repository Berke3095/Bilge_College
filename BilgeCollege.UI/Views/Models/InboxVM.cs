using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.UI.Views.Models
{
    public class InboxVM
    {
        public InboxVM()
        {
            Senders = new List<object>();
        }

        public List<Message> ReceivedMessages { get; set; }
        public List<object> Senders { get; set; }
    }
}
