

namespace Pizza.Repository.IRepository
{
    public interface IAuthRepository
    {
        public Task<RepositoryResponse<int>> Register(AuthRegister regUser);
        public Task<RepositoryResponse<string>> Login(AuthLogin user);
        public Task<bool> UserExists(string username);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}