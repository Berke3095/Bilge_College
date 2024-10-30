using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.DAL.Repository
{
    public interface I_Repository<T> where T : BaseEntity
    {
        public List<T> GetAll();
        public List<T> GetAllActives();
        public List<T> GetAllPassives();

        public T GetById(int id);
        public T GetByGuidId(string id);

        public void Create(T item);
        public void CreateRange(List<T> items);
        public void Update(T item);
        public void UpdateRange(List<T> items);
        public void Delete(T item);
        public void DeleteRange(List<T> items);
        public void Recover(T item);
        public void RecoverRange(List<T> items);
        public void Destroy(T item);
        public void DestroyRange(List<T> items);
    }
}
