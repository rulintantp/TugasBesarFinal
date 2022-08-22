using System.ComponentModel.DataAnnotations.Schema;

namespace MyASPProject.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public decimal TotalPage { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
