using Pizza.Common.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RepositoryResponse<int>>> Register(AuthRegister user)
        {
            var response = await _repository.Register(user);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<RepositoryResponse<UserResponse>>> Login(AuthLogin user)
        {
            var response = await _repository.Login(user);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}