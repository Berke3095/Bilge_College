using BilgeCollege.DAL.Context;
using BilgeCollege.MODELS.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.DAL.Repository
{
    public class Repository<T> : I_Repository<T> where T : BaseEntity
    {
        private readonly CollegeContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CollegeContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Delete(T item)
        {
            item.State = MODELS.Enums.StateEnum.Passive;
            Update(item);
        }

        public void DeleteRange(List<T> items)
        {
            foreach (T item in items)
            {
                item.State = MODELS.Enums.StateEnum.Passive;
            }
            UpdateRange(items);
        }

        public void Destroy(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void DestroyRange(List<T> items)
        {
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAllActives()
        {
            return _dbSet.Where(x => x.State == MODELS.Enums.StateEnum.Active).ToList();
        }

        public List<T> GetAllPassives()
        {
            return _dbSet.Where(x => x.State == MODELS.Enums.StateEnum.Passive).ToList();
        }

        public T GetByGuidId(string id)
        {
            return _dbSet.FirstOrDefault(x => x.GuidId == id);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Recover(T item)
        {
            item.State = MODELS.Enums.StateEnum.Active;
            Update(item);
        }

        public void RecoverRange(List<T> items)
        {
            foreach (T item in items)
            {
                item.State = MODELS.Enums.StateEnum.Active;
            }
            UpdateRange(items);
        }

        public void Update(T item)
        {
            item.ModifiedDate = DateTime.Now;

            T original = _dbSet.Find(item.Id);
            _context.Entry(original).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void UpdateRange(List<T> items)
        {
            foreach(T item in items)
            {
                item.ModifiedDate = DateTime.Now;

                T original = _dbSet.Find(item.Id);
                _context.Entry(original).CurrentValues.SetValues(item);
            }
            _context.SaveChanges();
        }
    }
}
