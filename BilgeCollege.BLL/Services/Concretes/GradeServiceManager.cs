using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class GradeServiceManager : BaseServiceManager<Grade>, I_GradeServiceManager
    {
        private readonly I_Repository<Grade> _repository;

        public GradeServiceManager(I_Repository<Grade> repository) : base(repository)
        {
            _repository = repository;
        }

        public void CreateRangeWithoutSave(List<Grade> gradesToSave)
        {
            _repository.GetDbSet().AddRange(gradesToSave);
        }

        public void DestroyRangeWithoutSave(List<Grade> gradesToDestroy)
        {
            _repository.GetDbSet().RemoveRange(gradesToDestroy);
        }
    }
}
