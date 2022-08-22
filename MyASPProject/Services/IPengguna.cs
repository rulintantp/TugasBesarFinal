namespace MyASPProject.Services
{
    public interface IPengguna
    {
        Task<IEnumerable<string>> GetAllUser();
        Task AddUserToRole(string username, string role);

    }
}
