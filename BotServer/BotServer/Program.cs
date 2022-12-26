using BotServer.Midlewares;
using BotServer.SignalR.Hubs;
using BotServer.SignalR_info.Hubs;
using Hangfire;
using Hellang.Middleware.ProblemDetails;
using NLog;
using NLog.Web;
using System.Reflection;


public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container

        var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("init main");

        try
        {
            Botserve.MigrationApp.DependencyInjection.AddMigraion(builder.Services, builder.Configuration);
            BotServer.Data.DependencyInjection.AddData(builder.Services, builder.Configuration);
            BotServer.Services.DependencyInjection.AddServices(builder.Services, builder.Configuration);
            BotServer.Features.DependensyInjection.AddFeatures(builder.Services, builder.Configuration);
            BotServer.DependencyInjection.AddBotServer(builder.Services, builder.Configuration, builder.Environment);
            BotServer.Comunications.DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);


            builder.Configuration.AddJsonFile("settings.json");


            // added nlog
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            var app = builder.Build();

            await BotServer.SetSampleData.SetData(app);

            //if (app.Environment.EnvironmentName == "Testing")
            //{
            //    app.UseMiddleware<DbTransactionMiddleware>();
            //}
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
          
            app.UseProblemDetails();
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
        }
        catch (Exception exception)
        {
            // NLog: catch setup errors
            logger.Error(exception, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }

    }

    
}


