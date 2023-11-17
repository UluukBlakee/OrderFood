namespace OrderFood.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int CafesId { get; set; }
        public Cafe? Cafes { get; set; }
    }
}
