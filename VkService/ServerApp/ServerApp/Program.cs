using MassTransit;
using ServerApp.Rabitmq;
using VkNet.Enums.SafetyEnums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
VkServer.Comunication.DependencyInjection.ConfigureServices(builder.Services,builder.Configuration);
//builder.Services.AddMassTransitHostedService();
builder.Configuration.AddJsonFile("settings.json");

var app = builder.Build();



//var bus = Bus.Factory.CreateUsingRabbitMq(conf =>
//{
//    conf.Host(new Uri("rbbitmq://guest:guest@localhost:15672"));

//});

//bus.Start();

//bus.Publish("hello");



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
