using MyASPProject.Models;

namespace MyASPProject.Services
{
    public interface ISamurai
    {
        Task<IEnumerable<Samurai>> GetAll(); 
        Task<Samurai> GetById(int id);
        Task<Samurai> Insert(Samurai obj);
        Task<Samurai> Update(Samurai obj);
        Task Delete(int id);
        Task<IEnumerable<Samurai>> GetByName(string name);
        Task<IEnumerable<SamuraiWithQuotes>> GetSamuraiWithQuotes();
        Task<IEnumerable<Weather>> GetAllWeather(string token);
    }
}
