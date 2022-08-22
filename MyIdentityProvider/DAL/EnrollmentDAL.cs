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
    public class EnrollmentDAL : IEnrollment
    {
        private readonly AppDbContext _context;
        public EnrollmentDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteEnrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentID == id);
                if (deleteEnrollment == null)
                    throw new Exception($"Data enrollment dengan id {id} tidak ditemukan");
                _context.Enrollments.Remove(deleteEnrollment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.Include("Course").Include("Student").OrderBy(e => e.EnrollmentID).ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var result = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentID == id);
            if (result == null) throw new Exception($"Data enrollment dengan id {id} tidak ditemukan");
            return result;
        }

    
        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _context.Enrollments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            try
            {
                var updateCourse = await _context.Enrollments.FirstOrDefaultAsync(e => e.CourseID == obj.CourseID);
                if (updateCourse == null)
                    throw new Exception($"Data course dengan id {obj.CourseID} tidak ditemukan");

                updateCourse.Grade = obj.Grade;
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
