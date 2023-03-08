using System.Text;

namespace Pizza.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RepositoryResponse<List<T>>> GetAllAsync() 
        {
            var response = new RepositoryResponse<List<T>>();

            response.Data = await _context.Set<T>().ToListAsync();

            return response;
        }
        
        public async Task<RepositoryResponse<T>> GetByIdAsync(int id) 
        {
            var response = new RepositoryResponse<T>();

            response.Data = await _context.Set<T>().FindAsync(id);

            if(response.Data is null)
                throw new NotFoundException(typeof(T).Name, id);

            return response;
        }

        public virtual async Task<RepositoryResponse<T>> CreateAsync<TSource>(TSource source) 
        {
            var response = new RepositoryResponse<T>();
            var entity = _mapper.Map<T>(source);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            response.Data = entity;

            return response;
        }

        public virtual async Task<RepositoryResponse<T>> UpdateAsync<TSource>(TSource source, int id) 
        {
            var response = new RepositoryResponse<T>();
            var entity = await _context.Set<T>()
                .FindAsync(id);

            if(entity == null )
                throw new NotFoundException(typeof(T).Name, id);

            _mapper.Map(source, entity);
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            response.Data = entity;

            return response;
        }

        public async Task<RepositoryResponse<List<T>>> DeleteAsync(int id) 
        {
            var response = new RepositoryResponse<List<T>>();
            var entity = await _context.Set<T>().FindAsync(id);
            
            if(entity is null)
                throw new NotFoundException(typeof(T).Name, id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            response.Data = await _context.Set<T>().ToListAsync();

            return response;
        }

        public List<T> SortEntities(string orderBy, List<T> entities)
        {
            var order = new StringBuilder(orderBy);
            
            order[0] = Convert.ToChar(order[0].ToString().ToUpper());
            var orderByInfo = order.ToString().Split('_');
            var propInfo = typeof(T).GetProperty(orderByInfo[0]);

            if(propInfo is null)
                return entities;
            
            var orderByAsc = entities.OrderBy(x => propInfo.GetValue(x)).ToList();
            var orderByDesc = entities.OrderByDescending(x => propInfo.GetValue(x)).ToList();

            return orderByInfo[1] == "desc" ? orderByDesc : orderByAsc;
        }
    }
}