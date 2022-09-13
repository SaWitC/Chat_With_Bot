

using BotServer.SignalR.Hubs;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(c =>
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
    builder =>
    {
        // builder.WithOrigins("http://example.com");
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddSignalR(opt =>
{
    opt.EnableDetailedErrors = true;
});
BotServer.Data.DependencyInjection.AddData(builder.Services,builder.Configuration);
BotServer.Services.DependencyInjection.AddServices(builder.Services,builder.Configuration);
BotServer.Features.DependensyInjection.AddFeatures(builder.Services);
BotServer.DependencyInjection.AddBotServer(builder.Services,builder.Configuration);

var app = builder.Build();

await BotServer.SetSampleData.SetData(app);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/BotServer/swagger.json", "BotServer");
        //c.RoutePrefix = string.Empty;
    });
}
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//app.UseSignalR(routes =>
//{
//    routes.MapHub<ChatHub>("/chatHub");
//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/toastr");
});

app.Run();
