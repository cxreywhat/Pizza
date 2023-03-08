namespace Pizza.Common.Authorization
{
    public interface IJwtUtils
    {
        public string CreateToken(UserModel user);    
        public int? ValidateJwtToken(string token);
    }
}