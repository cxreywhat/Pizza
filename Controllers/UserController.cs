using Microsoft.AspNetCore.Mvc;
using Pizza.Common.Authorization;

namespace Pizza.Controllers
{
    [Controller]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [Authorize(UserRole.Manager, UserRole.SuperAdmin, UserRole.Admin)]
        [HttpGet("GetAll")]
        public async Task<ActionResult<RepositoryResponse<List<UserResponse>>>> GetAllUsersAsync() 
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        [Authorize(UserRole.Manager, UserRole.SuperAdmin, UserRole.Admin)]
        [HttpGet("{id}")]
         public async Task<ActionResult<RepositoryResponse<UserResponse>>> GetUserByIdAsync(int id)
         {
            var response = await _userRepository.GetByIdAsync(id);

            if(response.Data is null)
                return NotFound($"Not found user with id: {id}");

            return Ok(response.Data);
         }

        [Authorize(UserRole.SuperAdmin, UserRole.Admin)]
        [HttpPost("Create")]
        public async Task<ActionResult<RepositoryResponse<UserResponse>>> CreateUserAsync(CreateUser user) 
        {
            return Ok(await _userRepository.CreateAsync(user));
        }

        [Authorize(UserRole.SuperAdmin, UserRole.Admin)]
        [HttpPut("Update")]
         public async Task<ActionResult<RepositoryResponse<UserResponse>>> UpdateUserAsync(int id, UpdateUser user)
         {
            var response = await _userRepository.UpdateAsync(user, id);

            if(response.Data is null)
                return NotFound($"Not found user with id: {id}");

            return Ok(response.Data);
         }

        [Authorize(UserRole.SuperAdmin, UserRole.Admin)]    
        [HttpDelete("Delete")]
         public async Task<ActionResult<RepositoryResponse<List<UserResponse>>>> DeleteUserAsync(int id)
         {
            var response = await _userRepository.DeleteAsync(id);

            if(response.Data is null)
                return NotFound($"Not found user with id: {id}");

            return Ok(response.Data);
         }
    }
}