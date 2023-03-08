namespace Pizza.Models.Pizza
{
    public class QueryParametersPizza
    {
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "rating_desc";
        public string Search { get; set; }  = string.Empty;
    }
}