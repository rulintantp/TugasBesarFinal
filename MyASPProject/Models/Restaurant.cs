namespace MyASPProject.Models
{
    public enum CuisineType
    {
        None,Italian,Indonesian,French,Germany
    }
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
