using MyASPProject.Models;

namespace MyASPProject.Services
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll(int pageNumber, string token);
        Task<Student> GetById(int id);
        Task<bool> Delete(int id, string token);
        Task<Student> Insert(Student obj, string token);
        Task<IEnumerable<Student>> GetByName(string name, string token);
        Task<Student> Update(Student obj, string token);
        public Task<IEnumerable<Enrollment>> GetEnrollmentByID(int id, string token);
    }
}
