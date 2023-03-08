using System.ComponentModel.DataAnnotations;

namespace Pizza.Models.Pizza
{
    public class PizzaModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Categories Category { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
    }
}