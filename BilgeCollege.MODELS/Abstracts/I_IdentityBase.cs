namespace BilgeCollege.MODELS.Abstracts
{
    public interface I_IdentityBase
    {
        public int CustomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
