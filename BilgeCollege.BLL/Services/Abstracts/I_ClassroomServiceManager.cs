using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_ClassroomServiceManager : I_BaseServiceManager<Classroom>
    {
        public string GenerateClassCode(I_ClassroomServiceManager classroomServiceManager, string grade);
    }
}
