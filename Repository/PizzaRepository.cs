using System.Security.Claims;

namespace Pizza.Repository
{
    public class PizzaRepository : GenericRepository<PizzaModel>, IPizzaRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly IMapper _mapper;

        public PizzaRepository(DataContext context, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor) : base(context, mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<RepositoryResponse<List<PizzaModel>>> GetByQueryParamsAsync(QueryParametersPizza parameters)
        {
            var response = new RepositoryResponse<List<PizzaModel>>();
            var pizzas = await _context.Pizzas.ToListAsync();

            if (!String.IsNullOrEmpty(parameters.Search))
            {
                pizzas = pizzas.Where(s => s.Title!
                    .ToLower()
                    .Contains(parameters.Search.ToLower())).ToList();
            }

            pizzas = pizzas.Where(p => 
                p.Category.ToString().Contains(parameters.Category) &&
                p.Type.ToString().Contains(parameters.Type) &&
                p.Size.ToString().Contains(parameters.Size) &&
                p.Price.ToString().Contains(parameters.Price) &&
                p.Rating.ToString().Contains(parameters.Rating)).ToList();
                

            response.Data = SortEntities(parameters.OrderBy, pizzas);

            return response;
        }

        public async Task<RepositoryResponse<List<PizzaModel>>> AddPizzaToUser(int pizzaId)
        {
            var response = new RepositoryResponse<List<PizzaModel>>();
            var user = (UserResponse)_httpContextAccessor.HttpContext!.Items["User"]!;
            var countPizzas = await _context.Pizzas.ToListAsync();
            var genericRepository = new GenericRepository<UserModel>(_context, _mapper);


            if(pizzaId > countPizzas.Count || pizzaId < 1)
            {
                response.Success = false;
                response.Message = $"?????? ?????????? ?? ???????????? id:{pizzaId}";
                return response;
            }

            var pizza = await _context.Pizzas.FindAsync(pizzaId);

            if(pizza is null)
                return response;

            user.Pizzas.Add(pizza);

            await genericRepository.UpdateAsync(user, user.Id);
            
            response.Data = user.Pizzas;

            return response;
        }
    }
}