using BotServer.Data.Data;
using BotServer.Domain.ConfigModels;
using BotServer.Domain.Models;
using BotServer.Features;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace BotServer
{
    public class DependencyInjection
    {
        public static void AddBotServer(IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddControllers().AddNewtonsoftJson(); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();


            Services.AddValidatorsFromAssembly(typeof(startupFeatures).Assembly);
            Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("BotServer", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

            Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    // builder.WithOrigins("http://example.com");
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            Services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
            });




            var authoptions = configuration.GetSection("auth");
            Services.Configure<AuthOptions>(authoptions);

            // Services.AddAutoMapper(typeof(BotServer.Features.startup));


            var AuthOptionsForVlidation = configuration.GetSection("auth").Get<AuthOptions>();

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
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // если запрос направлен хабу
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/toastr") || path.StartsWithSegments("/notify")))
                        {
                            // получаем токен из строки запроса
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }

                };
            });
        }
    }
}
