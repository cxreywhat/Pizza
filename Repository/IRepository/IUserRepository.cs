namespace Pizza.Repository.IRepository
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        public Task<RepositoryResponse<UserResponse>> CreateAsync(CreateUser newUser);
        public Task<RepositoryResponse<UserResponse>> UpdateAsync(UpdateUser updUser, int id);
    }
}