using Microsoft.EntityFrameworkCore;
using MyIdentityProvider.DAL;
using StudentCourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourse.DATA.DAL
{
    public class CourseDAL : ICourse
    {
        private readonly AppDbContext _context;
        public CourseDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteCourse = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == id);
                if (deleteCourse == null)
                    throw new Exception($"Data course dengan id {id} tidak ditemukan");
                _context.Courses.Remove(deleteCourse);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(c => c.CourseID).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
            if (result == null) throw new Exception($"Data courses dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Course>> GetByName(string name)
        {
            var courses = await _context.Courses.Where(c => c.Title.Contains(name))
                .OrderBy(s => s.Title).ToListAsync();
            return courses;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentByCourseId(int id)
        {
            var enrollment = await _context.Enrollments.Include("Course").Include("Student").Where(e => e.CourseID == id).OrderBy(e => e.Course.Title).ToListAsync();
            return enrollment;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Course> Update(Course obj)
        {
            try
            {
                var updateCourse = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == obj.CourseID);
                if (updateCourse == null)
                    throw new Exception($"Data course dengan id {obj.CourseID} tidak ditemukan");

                updateCourse.Title = obj.Title;
                updateCourse.Credits = obj.Credits;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
