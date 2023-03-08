namespace Pizza.Repository.IRepository
{
    public interface IPizzaRepository : IGenericRepository<PizzaModel>
    {
        public Task<RepositoryResponse<List<PizzaModel>>> GetByQueryParamsAsync(QueryParametersPizza parameters);
        public Task<RepositoryResponse<List<PizzaModel>>> AddPizzaToUser(int pizzaId);
    }
}