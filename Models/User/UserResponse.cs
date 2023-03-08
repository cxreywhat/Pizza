using System.Globalization;

namespace Pizza.Models.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Role { get; set; } = string.Empty;
        public List<PizzaModel> Pizzas { get; set; } = new List<PizzaModel>();
    }
}