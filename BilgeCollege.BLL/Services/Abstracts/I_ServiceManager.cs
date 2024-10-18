using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_ServiceManager<T> where T : BaseEntity
    {
        public List<T> GetAll();
        public List<T> GetAllActives();
        public List<T> GetAllPassives();

        public T GetById(int id);

        public void Create(T item);
        public void Update(T item);
        public void Delete(T item);
        public void Recover(T item);
        public void Destroy(T item);
    }
}
