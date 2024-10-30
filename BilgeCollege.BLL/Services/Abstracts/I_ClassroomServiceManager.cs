using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Abstracts
{
    public interface I_ClassroomServiceManager : I_BaseServiceManager<Classroom>
    {
        public string GenerateClassCode(I_ClassroomServiceManager classroomServiceManager, string grade);
        public void HandleOnDestroy(int id, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager);
        public void HandleOnDestroyAll(I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager);
        public void HandleOnDelete(int id, I_StudentServiceManager studentServiceManager);
        public void HandleOnDeleteAll(I_StudentServiceManager studentServiceManager);

        public List<int> GetAllAltTopicIds(int id, I_DayScheduleServiceManager dayScheduleServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_ClassHourServiceManager classHourServiceManager);
        public void HandleRemovedAltTopics(Classroom classroom, List<int> oldAltTopics, List<int> newAltTopics, I_GradeServiceManager gradeServiceManager, I_StudentServiceManager studentServiceManager);
        public void HandleAltTopics(Classroom classroom, List<int> oldAltTopics, List<int> newAltTopics, I_GradeServiceManager gradeServiceManager, I_StudentServiceManager studentServiceManager);
    }
}
