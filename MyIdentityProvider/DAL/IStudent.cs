using StudentCourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourse.DATA.DAL
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string name);
        Task<IEnumerable<Enrollment>> GetEnrollmentByID(int id);
    }
}
