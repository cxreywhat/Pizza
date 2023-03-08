using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pizza.Common.Authorization;
using Pizza.Repository;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<DataContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddCors();
    builder.Services.AddControllers().AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    
    builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
    builder.Services.AddScoped<IAuthRepository, AuthRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IJwtUtils, JwtUtils>();
    builder.Services.AddHttpContextAccessor();

    // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //     .AddJwtBearer(options =>
    //         {
    //             options.TokenValidationParameters = new TokenValidationParameters
    //             {
    //                 ValidateIssuerSigningKey = true,
    //                 IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
    //                         .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
    //                 ValidateIssuer = false,
    //                 ValidateAudience = false
    //             };
    //         });
}


var app = builder.Build();
{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
            
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
}

app.Run("http://localhost:5000");