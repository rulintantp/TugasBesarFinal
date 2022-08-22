using StudentCourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourse.DATA.DAL
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByName(string name);
        public Task<IEnumerable<Enrollment>> GetEnrollmentByCourseId(int id);
        
    }

}
