using BilgeCollege.MODELS.Enums;

namespace BilgeCollege.MODELS.Abstracts
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            GuidId = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            State = StateEnum.Active;
        }

        public int Id { get; set; }
        public string GuidId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public StateEnum State { get; set; }
    }
}
