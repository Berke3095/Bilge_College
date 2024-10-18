namespace BilgeCollege.MODELS.Abstracts
{
    public interface I_IdentityBase // Used for Asp Identity custom classes
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
