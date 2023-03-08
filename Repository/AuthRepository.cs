using Pizza.Common.Authorization;

namespace Pizza.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _utils;
        private readonly IMapper _mapper;

        public AuthRepository(DataContext context, IConfiguration configuration, IJwtUtils utils, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _utils = utils;
            _mapper = mapper;
        }

        public async Task<RepositoryResponse<List<UserModel>>> GetAllUsers()
        {
            var response = new RepositoryResponse<List<UserModel>>(); 
            var users = await _context.Users.ToListAsync();

            response.Data = users;
            return response;
        }

        public async Task<RepositoryResponse<string>> Login(AuthLogin logUser)
        {
            var response = new RepositoryResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(logUser.Username.ToLower()));
            
            if (user is null)
            {
                response.Success = false;
                response.Message = "Пользователь не найден.";
            }
            else if (!VerifyPasswordHash(logUser.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Неверный пароль.";
            }
            else
            {
                response.Data = _utils.CreateToken(user);
            }

            return response;
        }

        public async Task<RepositoryResponse<int>> Register(AuthRegister regUser)
        {
            var response = new RepositoryResponse<int>();

            if (await UserExists(regUser.Username))
            {
                response.Success = false;
                response.Message = "Данный пользователь уже существует.";
                return response;
            }

            CreatePasswordHash(regUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<UserModel>(regUser);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
                return true;

            return false;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}