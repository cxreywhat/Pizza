namespace Pizza.Models.Pizza
{
    public class UpdatePizza
    {
        public string Title { get; set; } = string.Empty;
        public Categories Category { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Type { get; set; }
        public double Size { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
    }
}