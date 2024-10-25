﻿using BilgeCollege.BLL.Services.Abstracts;
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

            return null;
        }
    }
}
