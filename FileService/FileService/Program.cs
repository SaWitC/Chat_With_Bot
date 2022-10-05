var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


FileServer.Application.DependencyInjection.AddApplication(builder.Configuration,builder.Services);
FileServer.Data.DependencyInjection.AddData(builder.Configuration, builder.Services);
FileServer.Domain.DependencyInjection.AddDomain(builder.Configuration, builder.Services);
FileServer.Features.DependencyInjection.AddFeatures(builder.Configuration, builder.Services);
FileServer.Migrations.DependencyInjection.AddMigrations(builder.Configuration, builder.Services);
FileServer.Services.DependencyInjection.AddServices(builder.Configuration, builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
