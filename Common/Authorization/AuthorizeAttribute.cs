using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pizza.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserRole[] _roles;

        public AuthorizeAttribute(params UserRole[] roles)
        {
            _roles = roles ?? new UserRole[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserModel)context.HttpContext.Items["User"];
            if (user is null)
            {
                context.Result = new JsonResult(new { message = "Для доступа нужно авторизоваться!" }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }
            if(_roles.Any() && !_roles.Contains(user!.Role))
            {
                context.Result = new JsonResult(new { message = "У тебя нет доступа к данной функции!" }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized 
                };
            }   
        }
    }
}