using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Concretes.CustomUser;

namespace BilgeCollege.MODELS.Concretes
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }

        // Relations
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
