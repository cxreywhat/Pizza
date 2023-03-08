using System.Globalization;

namespace Pizza.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int? Age { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public List<PizzaModel>? Pizzas { get; set; } = new List<PizzaModel>();
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}