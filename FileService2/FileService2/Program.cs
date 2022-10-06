using BotServer.Domain.ConfigModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//FileServer.Application.DependencyInjection.AddApplication(builder.Configuration, builder.Services);
FileServer.Data.DependencyInjection.AddData(builder.Configuration, builder.Services);
//FileServer.Domain.DependencyInjection.AddDomain(builder.Configuration, builder.Services);
FileServer.Features.DependencyInjection.AddFeatures(builder.Configuration, builder.Services);
FileServer.Migrations.DependencyInjection.AddMigrations(builder.Configuration, builder.Services);
FileServer.Services.DependencyInjection.AddServices(builder.Configuration, builder.Services);

var authoptions = builder.Configuration.GetSection("auth");
builder.Services.Configure<AuthOptions>(authoptions);

// Services.AddAutoMapper(typeof(BotServer.Features.startup));

var AuthOptionsForVlidation = builder.Configuration.GetSection("auth").Get<AuthOptions>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
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
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
