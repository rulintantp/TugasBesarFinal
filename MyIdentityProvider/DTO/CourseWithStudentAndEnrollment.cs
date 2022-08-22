using StudentCourse.Domain;

namespace StudentCourseWebAPI.DTO
{
    public class CourseWithStudentAndEnrollment
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
