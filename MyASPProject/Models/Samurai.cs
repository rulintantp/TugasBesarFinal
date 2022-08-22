using System.ComponentModel.DataAnnotations;

namespace MyASPProject.Models
{
    public class Samurai
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
