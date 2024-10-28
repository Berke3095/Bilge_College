using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_ClassroomServiceManager : I_BaseServiceManager<Classroom>
    {
        public string GenerateClassCode(I_ClassroomServiceManager classroomServiceManager, string grade);
        public void HandleOnDestroy(int id, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager);
        public void HandleOnDestroyAll(I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager);
    }
}
