namespace BilgeCollege.MODELS.Abstracts
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            GuidId = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string GuidId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
