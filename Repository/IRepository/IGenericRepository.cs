namespace Pizza.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<RepositoryResponse<List<T>>> GetAllAsync();
        public Task<RepositoryResponse<T>> GetByIdAsync(int id);
        public Task<RepositoryResponse<T>> CreateAsync<TSource>(TSource source);
        public Task<RepositoryResponse<T>> UpdateAsync<TSource>(TSource source, int id);
        public Task<RepositoryResponse<List<T>>> DeleteAsync(int id);
        public List<T> SortEntities(string orderBy, List<T> entities);
    }
}