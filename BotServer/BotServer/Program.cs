using BotServer.SignalR.Hubs;
using BotServer.SignalR_info.Hubs;
using FluentValidation;
using Hangfire;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

Botserve.MigrationApp.DependencyInjection.AddMigraion(builder.Services,builder.Configuration);
BotServer.Data.DependencyInjection.AddData(builder.Services,builder.Configuration);
BotServer.Services.DependencyInjection.AddServices(builder.Services,builder.Configuration);
BotServer.Features.DependensyInjection.AddFeatures(builder.Services,builder.Configuration);
BotServer.DependencyInjection.AddBotServer(builder.Services,builder.Configuration);

builder.Configuration.AddJsonFile("settings.json");
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
app.UseHangfireDashboard("/dashboard");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapHub<HubForNotify>("/notify");
    endpoints.MapHub<ChatHub>("/toastr");
    
});

app.Run();
