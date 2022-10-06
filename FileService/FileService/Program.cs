using FileServer.Domain.Models.OptionsModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


FileServer.Application.DependencyInjection.AddApplication(builder.Configuration,builder.Services);
FileServer.Data.DependencyInjection.AddData(builder.Configuration, builder.Services);
FileServer.Domain.DependencyInjection.AddDomain(builder.Configuration, builder.Services);
FileServer.Features.DependencyInjection.AddFeatures(builder.Configuration, builder.Services);
FileServer.Migrations.DependencyInjection.AddMigrations(builder.Configuration, builder.Services);
FileServer.Services.DependencyInjection.AddServices(builder.Configuration, builder.Services);

//Authorization
var authoptions = builder.Configuration.GetSection("auth");
builder.Services.Configure<AuthOptions>(authoptions);

var AuthOptionsForVlidation = builder.Configuration.GetSection("auth").Get<AuthOptions>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(conf =>
{
    conf.RequireHttpsMetadata = true;
    conf.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
