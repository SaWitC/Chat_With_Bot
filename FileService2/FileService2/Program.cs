using BotServer.Domain.ConfigModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
   
});

FileServer.Data.DependencyInjection.AddData(builder.Configuration, builder.Services);
FileServer.Features.DependencyInjection.AddFeatures(builder.Configuration, builder.Services);
FileServer.Migrations.DependencyInjection.AddMigrations(builder.Configuration, builder.Services);
FileServer.Services.DependencyInjection.AddServices(builder.Configuration, builder.Services);




var authoptions = builder.Configuration.GetSection("auth");
builder.Services.Configure<AuthOptions>(authoptions);

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
         ValidAudiences = AuthOptionsForVlidation.Audience,
     };
 });

/// cors
builder.Services.AddCors(o =>
{
    o.AddPolicy("test", p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowAnyOrigin();
    });
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
    });
}

app.UseCors("test");

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
