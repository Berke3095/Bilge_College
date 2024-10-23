using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class BaseServiceManager<T> : I_BaseServiceManager<T> where T : BaseEntity
    {
        private readonly I_Repository<T> _repository;

        public BaseServiceManager(I_Repository<T> repository)
        {
            _repository = repository;
        }

        public void Create(T item)
        {
            if (item != null) _repository.Create(item);
            else throw new Exception("Item trying to create is null!");
        }

        public void Delete(T item)
        {
            if(item != null) _repository.Delete(item);
            else throw new Exception("Item trying to delete is null!");
        }

        public void DeleteRange(List<T> items)
        {
            if(items.Count() > 0) _repository.DeleteRange(items);
        }

        public void Destroy(T item)
        {
            if(item != null) _repository.Destroy(item);
            else throw new Exception("Item trying to destroy is null!");
        }

        public void DestroyRange(List<T> items)
        {
            if (items.Count() > 0) _repository.DestroyRange(items);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public List<T> GetAllActives()
        {
            return _repository.GetAllActives();
        }

        public List<T> GetAllPassives()
        {
            return _repository.GetAllPassives();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Recover(T item)
        {
            if(item != null) _repository.Recover(item);
            else throw new Exception("Item trying to recover is null!");
        }

        public void RecoverRange(List<T> items)
        {
            if (items.Count() > 0) _repository.RecoverRange(items);
        }

        public void Update(T item)
        {
            if (item != null) _repository.Update(item);
            else throw new Exception("Item trying to update is null!");
        }

        public void UpdateRange(List<T> items)
        {
            if (items.Count() > 0) _repository.UpdateRange(items);
        }
    }
}
