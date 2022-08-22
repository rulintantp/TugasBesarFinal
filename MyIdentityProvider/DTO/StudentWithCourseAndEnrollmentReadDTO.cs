using StudentCourse.Domain;

namespace StudentCourseWebAPI.DTO
{
    public class StudentWithCourseAndEnrollmentReadDTO
    {
        public int ID { get; set; }
        public string FirstMidName  { get; set; }
        public string LastName  { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public decimal TotalPage { get; set; }
    }
}
