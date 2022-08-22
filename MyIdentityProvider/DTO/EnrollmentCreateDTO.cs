

using StudentCourse.Domain;

namespace StudentCourseWebAPI.DTO
{
    public class EnrollmentCreateDTO
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade Grade { get; set; }
        
    }
}
