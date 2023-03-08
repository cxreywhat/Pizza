using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Pizza.Common.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository repository, IJwtUtils utils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine(token);
            var userId = utils.ValidateJwtToken(token);
            Console.WriteLine(userId);
            if (userId != null)
            {
                var response = await repository.GetByIdAsync(userId.Value);
                context.Items["User"] = response.Data;
            }

            await _next(context);
        }
    }
}