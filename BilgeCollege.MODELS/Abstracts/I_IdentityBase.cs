namespace BilgeCollege.MODELS.Abstracts
{
    public interface I_IdentityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
