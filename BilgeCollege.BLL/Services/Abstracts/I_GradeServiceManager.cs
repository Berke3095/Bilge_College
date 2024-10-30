using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_GradeServiceManager : I_BaseServiceManager<Grade>
    {
        public void CreateRangeWithoutSave(List<Grade> gradesToSave);
        public void DestroyRangeWithoutSave(List<Grade> gradesToDestroy);
    }
}
