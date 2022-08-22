using StudentCourse.Domain;

namespace StudentCourseWebAPI.DTO
{
    public class EnrollmentReadDTO
    {
        public int EnrollmentID { get; set; }
        public Grade? Grade { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal TotalPage { get; set; }
        public string CourseTitle { get; set; }
        public string StudentName { get; set; }
    }
}
