using MyASPProject.Models;

namespace MyASPProject.Services
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAll(int pageNumber, string token);
        Task<Course> GetById(int id);
        Task<bool> Delete(int id, string token);
        Task<Course> Insert(Course obj, string token);
        Task<IEnumerable<Course>> GetByName(string name, string token);
        Task<Course> Update(Course obj, string token);
        public Task<IEnumerable<Enrollment>> GetEnrollmentByCourseId(int id, string token);

    }
}
