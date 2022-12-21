using BotServer.Data.Data;
using BotServer.Domain.ConfigModels;
using BotServer.Domain.Models;
using BotServer.Features;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace BotServer
{
    public class DependencyInjection
    {
        public static void AddBotServer(IServiceCollection services, IConfiguration configuration,IWebHostEnvironment env)
        {
            //problem details 
            services.AddProblemDetails(o =>
            {
                o.IncludeExceptionDetails = (ctx, cfg) => env.IsDevelopment();
                // o.MapStatusCode =StatusCodes.Status400BadRequest;
                o.MapToStatusCode<Exception>(400);
            });

            services.AddControllers().AddNewtonsoftJson(); ;
            services.AddEndpointsApiExplorer();


            services.AddValidatorsFromAssembly(typeof(startupFeatures).Assembly);
            services.AddSwaggerGen(c =>
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

            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
            });




            var authoptions = configuration.GetSection("auth");
            if (env.EnvironmentName != "Testing")
            {
                services.AddIdentity<User, IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>();
            }
                services.Configure<AuthOptions>(authoptions);

                var AuthOptionsForVlidation = configuration.GetSection("auth").Get<AuthOptions>();


            

               

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
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
                        //ValidAudience = AuthOptionsForVlidation.Audience

                        ValidAudiences = AuthOptionsForVlidation.Audience,
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            //is request to hub
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/toastr") || path.StartsWithSegments("/notify")))
                            {
                                //get token from the request
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }

                    };
                });
            
         
        }
    }
}
