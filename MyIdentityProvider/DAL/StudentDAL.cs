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
    public class StudentDAL : IStudent
    {
        private readonly AppDbContext _context;
        public StudentDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteStudent = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                if (deleteStudent == null)
                    throw new Exception($"Data student dengan id {id} tidak ditemukan");
                _context.Students.Remove(deleteStudent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.ID).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Data student dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Student>> GetByName(string name)
        {
            var students = await _context.Students.Where(s => s.FirstMidName.Contains(name))
                .OrderBy(s => s.FirstMidName).ToListAsync();
            return students;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Student> Update(Student obj)
        {
            try
            {
                var updateStudent = await _context.Students.FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (updateStudent == null)
                    throw new Exception($"Data student dengan id {obj.ID} tidak ditemukan");

                updateStudent.FirstMidName = obj.FirstMidName;
                updateStudent.LastName = obj.LastName;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentByID(int id)
        {
            var enrollment = await _context.Enrollments.Include("Course").Include("Student").Where(e => e.StudentID == id).OrderBy(e => e.Student.FirstMidName).ToListAsync();
            return enrollment;
        }
    }
}
