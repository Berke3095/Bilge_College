using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Repository;
using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.BLL.Services.Concretes
{
    public class ClassroomServiceManager : BaseServiceManager<Classroom>, I_ClassroomServiceManager
    {
        private readonly I_Repository<Classroom> _repository;

        public ClassroomServiceManager(I_Repository<Classroom> repository) : base(repository)
        {
            _repository = repository;
        }

        public string GenerateClassCode(I_ClassroomServiceManager classroomServiceManager, string grade)
        {
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

            var namedClassrooms = classroomServiceManager.GetAllActives().Where(x => x.Grade != null);
            var classrooms = namedClassrooms.Where(x => x.Grade.Split('-')[0] == grade).ToList();

            List<string> classCodes = new List<string>();

            foreach (var item in classrooms)
            {
                if (item.Grade != null)
                {
                    classCodes.Add(item.Grade.ToString());
                }
            }

            string bestCode = grade + "-" + alphabet[0].ToString();

            if (classCodes.Contains(bestCode))
            {
                int i = 1;

                while (true)
                {
                    bestCode = grade + "-" + alphabet[i].ToString();

                    if (!classCodes.Contains(bestCode))
                    {
                        return bestCode;
                    }
                    else i++;
                }
            }
            else return bestCode;
        }

        public void HandleAltTopics(Classroom classroom, List<int> oldAltTopics, List<int> newAltTopics, I_GradeServiceManager gradeServiceManager, I_StudentServiceManager studentServiceManager)
        {
            HandleRemovedAltTopics(classroom, oldAltTopics, newAltTopics, gradeServiceManager, studentServiceManager);

            var addedAlts = new List<int>();

            foreach (var item in newAltTopics)
            {
                if (!oldAltTopics.Contains(item))
                {
                    addedAlts.Add(item);
                }
            }

            var gradesToCreate = new List<Grade>();

            var students = studentServiceManager.GetAllActives().Where(x => x.ClassroomId == classroom.Id).ToList();
            foreach (var student in students)
            {
                foreach (var item in addedAlts)
                {
                    Grade grade = new Grade
                    {
                        AltTopicId = item,
                        StudentId = student.Id,
                    };

                    gradesToCreate.Add(grade);
                }
            }

            gradeServiceManager.CreateRange(gradesToCreate);
        }

        public List<int> GetAllAltTopicIds(int id, I_DayScheduleServiceManager dayScheduleServiceManager, I_AltTopicServiceManager altTopicServiceManager, I_ClassHourServiceManager classHourServiceManager)
        {
            List<int> altTopicIds = new List<int>();

            var daySchedules = dayScheduleServiceManager.GetAll().Where(x => x.ClassroomId == id).ToList();
            foreach (var daySchedule in daySchedules)
            {
                var classHours = classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id);
                foreach (var classHour in classHours)
                {
                    var altTopicId = (int)classHour.AltTopicId;
                    if (altTopicId != 1 && !altTopicIds.Contains(altTopicId)) // If not none and doesnt have duplicate
                    {
                        altTopicIds.Add(altTopicId);
                    }
                }
            }

            return altTopicIds;
        }

        public void HandleRemovedAltTopics(Classroom classroom, List<int> oldAltTopics, List<int> newAltTopics, I_GradeServiceManager gradeServiceManager, I_StudentServiceManager studentServiceManager)
        {
            var removedAlts = new List<int>();

            foreach (var item in oldAltTopics)
            {
                if (!newAltTopics.Contains(item))
                {
                    removedAlts.Add(item);
                }
            }

            List<Grade> gradesToDestroy = new List<Grade>();

            var students = studentServiceManager.GetAllActives().Where(x => x.ClassroomId == classroom.Id);
            foreach (var student in students)
            {
                foreach (var item in removedAlts)
                {
                    if(gradeServiceManager.GetAll().Count() > 0)
                    {
                        var grade = gradeServiceManager.GetAll().First(x => x.AltTopicId == item && x.StudentId == student.Id);
                        gradesToDestroy.Add(grade);
                    }
                }
            }

            if(gradesToDestroy.Count() > 0)
            {
                gradeServiceManager.DestroyRange(gradesToDestroy);
            }
        }

        public void HandleOnDelete(int id, I_StudentServiceManager studentServiceManager)
        {
            var students = studentServiceManager.GetAllActives().Where(x => x.ClassroomId == id);
            foreach (var student in students)
            {
                student.ClassroomId = null;
            }

            var classroom = _repository.GetAllActives().First(x => x.Id == id);
            classroom.TotalStudents = 0;
        }

        public void HandleOnDeleteAll(I_StudentServiceManager studentServiceManager)
        {
            var students = studentServiceManager.GetAllActives();
            foreach (var student in students)
            {
                student.ClassroomId = null;
            }

            var classrooms = _repository.GetAllActives();
            foreach (var classroom in classrooms)
            {
                classroom.TotalStudents = 0;
            }
        }

        public void HandleOnDestroy(int id, I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager)
        {
            var daySchedules = dayScheduleServiceManager.GetAll().Where(x => x.ClassroomId == id).ToList();
            foreach (var daySchedule in daySchedules)
            {
                var classHours = classHourServiceManager.GetAll().Where(x => x.DayScheduleId == daySchedule.Id).ToList();
                classHourServiceManager.DestroyRange(classHours);
            }
            dayScheduleServiceManager.DestroyRange(daySchedules);
        }

        public void HandleOnDestroyAll(I_DayScheduleServiceManager dayScheduleServiceManager, I_ClassHourServiceManager classHourServiceManager)
        {
            classHourServiceManager.DestroyRange(classHourServiceManager.GetAll());
            dayScheduleServiceManager.DestroyRange(dayScheduleServiceManager.GetAll());
        }
    }
}
