using StudentCourse.Domain;

namespace StudentCourseWebAPI.DTO
{
    public class CourseReadDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public decimal TotalPage { get; set; }
       
    }
}
