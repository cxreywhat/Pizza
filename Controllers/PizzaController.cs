using Microsoft.AspNetCore.Mvc;
using Pizza.Common.Authorization;

namespace Pizza.Controllers
{
    [Controller]
    [Route("/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<RepositoryResponse<List<PizzaModel>>>> GetAllPizzasAsync() 
        {
            return Ok(await _pizzaRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<RepositoryResponse<PizzaModel>>> GetPizzaByIdAsync(int id)
         {
            var response = await _pizzaRepository.GetByIdAsync(id);

            if(response.Data is null)
                return NotFound($"Not found pizza with id: {id}");

            return Ok(response.Data);
        }

        [HttpGet()]
        public async Task<ActionResult<RepositoryResponse<PizzaModel>>> GetPizzasByParamsAsync(QueryParametersPizza parameters) 
        {
            return Ok(await _pizzaRepository.GetByQueryParamsAsync(parameters));
        }

         [Authorize(UserRole.SuperAdmin, UserRole.Admin)]
        [HttpPost("Create")]
        public async Task<ActionResult<RepositoryResponse<PizzaModel>>> CreatePizzasAsync(CreatePizza newPizza) 
        {
            return Ok(await _pizzaRepository.CreateAsync(newPizza));
        }

         [Authorize(UserRole.SuperAdmin, UserRole.Admin)]
        [HttpPut("Update")]
         public async Task<ActionResult<RepositoryResponse<PizzaModel>>> UpdatePizzaAsync(int id, UpdatePizza updatePizza)
         {
            var response = await _pizzaRepository.UpdateAsync(updatePizza, id);

            if(response.Data is null)
                return NotFound($"Not found pizza with id: {id}");

            return Ok(response.Data);
         }

         [Authorize(UserRole.SuperAdmin, UserRole.Admin)]
        [HttpDelete("Delete")]
         public async Task<ActionResult<RepositoryResponse<List<PizzaModel>>>> DeletePizzaAsync(int id)
         {
            var response = await _pizzaRepository.DeleteAsync(id);

            if(response.Data is null)
                return NotFound($"Not found pizza with id: {id}");

            return Ok(response.Data);
        }

        [Authorize(UserRole.User, UserRole.Employee, UserRole.Manager, UserRole.SuperAdmin, UserRole.Admin)]
        [HttpPost("AddPizza")]
        public async  Task<ActionResult<RepositoryResponse<List<PizzaModel>>>> AddPizzaToUser(int pizzaId)
        {
            return Ok(await _pizzaRepository.AddPizzaToUser(pizzaId));
        }
        
    }
}