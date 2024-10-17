using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.MODELS.Utils
{
    public static class SchoolNoGenerator
    {
        private static int _studentEnrollCount { get; set; } = 100; // Starting the count for the new year enrolls

        public static string GetSchoolNo()
        {
            string schoolNo = _studentEnrollCount.ToString();
            _studentEnrollCount++; // Increment everytime a student enrolls, this will have to be resetted after the enrolls close.

            string enrollYear = DateTime.Now.Year.ToString(); // 2024
            string lastTwoOfYear = enrollYear.Substring(2); // 24

            return schoolNo + "-" + lastTwoOfYear; // 100-24
        }

        public static void ResetEnrollCount()
        {
            _studentEnrollCount = 100; // Reset to 100, for enroll season closure
        }
    }
}
