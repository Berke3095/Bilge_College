using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Grade : BaseEntity
    {
        private double _midtermGrade;
        private double _finalGrade;
        private double _performanceGrade;

        public double MidTermGrade 
        { 
            get
            {
                return _midtermGrade;
            }
            set
            {
                if(value < 0) _midtermGrade = 0;
                else if(value > 100) _midtermGrade = 100;
                else _midtermGrade = value;
            }
        }
        public double FinalGrade
        {
            get
            {
                return _finalGrade;
            }
            set
            {
                if (value < 0) _finalGrade = 0;
                else if (value > 100) _finalGrade = 100;
                else _finalGrade = value;
            }
        }
        public double PerformanceGrade
        {
            get
            {
                return _performanceGrade;
            }
            set
            {
                if (value < 0) _performanceGrade = 0;
                else if (value > 100) _performanceGrade = 100;
                else _performanceGrade = value;
            }
        }
        public double Score
        {
            get
            {
                return ((MidTermGrade * 0.40) + (FinalGrade * 0.60) + PerformanceGrade) / 2;
            }
        }

        // Relations
        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }

        public Student? Student { get; set; }
        public int? StudentId { get; set; }
    }
}
