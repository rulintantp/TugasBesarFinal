using MyASPProject.Models;

namespace MyASPProject.Services
{
    public interface IEnrollment
    {
        Task<IEnumerable<Enrollment>> GetAll(int pageNumber, string token);
        Task<bool> Delete(int id, string token);
        Task<Enrollment> Insert(Enrollment obj, string token);
        Task<IEnumerable<Enrollment>> GetByName(string name, string token);
        Task<Enrollment> Update(Enrollment obj, string token);
    }
}
