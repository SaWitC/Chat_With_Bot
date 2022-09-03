using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Data.Repositories;
using BotServer.Domain.ConfigModels;
using BotServer.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BotServer
{
    public class DependencyInjection
    {
        public static void AddBotServer(IServiceCollection Services,IConfiguration configuration)
        {
            var authoptions = configuration.GetSection("auth");
            Services.Configure<AuthOptions>(authoptions);

            Services.AddScoped<IBaseRepository, BaseRepository>();
            Services.AddScoped<IAccountRepository, AccountRepository>();


            var AuthOptionsForVlidation = configuration.GetSection("auth").Get<AuthOptions>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = AuthOptionsForVlidation.GetSymetricSecurityKey(),
                    ValidIssuer = AuthOptionsForVlidation.Issuer,
                    ValidAudience = AuthOptionsForVlidation.Audience
                };

            });
        }
    }
}
