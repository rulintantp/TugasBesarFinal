using MyASPProject.Models;

namespace MyASPProject.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
