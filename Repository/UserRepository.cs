namespace Pizza.Repository
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public UserRepository(DataContext context, IMapper mapper, IAuthRepository authRepository)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _authRepository = authRepository;
        }

        public async Task<RepositoryResponse<UserResponse>> CreateAsync(CreateUser newUser)
        {
            var response = new RepositoryResponse<UserResponse>();
            if(await _authRepository.UserExists(newUser.Username))
                return response;

            var user = _mapper.Map<UserModel>(newUser);

            _authRepository.CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<UserResponse>(user);

            return response;
        }

        public async Task<RepositoryResponse<UserResponse>> UpdateAsync(UpdateUser updUser, int id)
        {
            var response = new RepositoryResponse<UserResponse>();
            var user = await _context.Users.FindAsync(id);

            if(user is null)
                throw new NotFoundException(id);

            _mapper.Map(updUser, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<UserResponse>(user);

            return response;
        }
    }
}