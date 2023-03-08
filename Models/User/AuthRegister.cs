using System.Globalization;

namespace Pizza.Models.User
{
    public class AuthRegister
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int? Age { get; set; }
    }
}