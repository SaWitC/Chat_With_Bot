using MassTransit;
using Microsoft.Extensions.Configuration;
using ServerApp.Models;

namespace ServerApp.Rabitmq
{
    public class ConfigureServicesMassTransit
    {
        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        //public async static Task ConfigureServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    var massTransitSection = configuration.GetSection("MassTransit");
        //    var url = massTransitSection.GetValue<string>("Url");
        //    var host = massTransitSection.GetValue<string>("Host");
        //    var userName = massTransitSection.GetValue<string>("UserName");
        //    var password = massTransitSection.GetValue<string>("Password");
        //    if (massTransitSection == null || url == null || host == null)
        //    {
        //        throw new ArgumentNullException("Section 'mass-transit' configuration settings are not found in appSettings.json");
        //    }

        //    services.AddMassTransit(x =>
        //    {
        //        x.AddBus(bf =>
        //        {
        //            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        //            {
        //                cfg.Host($"rabbitmq://{url}/{host}", configurator =>
        //                {
        //                    configurator.Username(userName);
        //                    configurator.Password(password);
        //                });

        //                cfg.ClearMessageDeserializers();
        //                cfg.UseRawJsonSerializer();
        //                //cfg.UseHealthCheck(context);
        //                cfg.ConfigureEndpoints(bf, SnakeCaseEndpointNameFormatter.Instance);

        //                cfg.ReceiveEndpoint("MyQueue",e =>
        //                {
        //                    e.Consumer<MyCustomConsumer>();
        //                });
        //            });
        //            return bus;
        //        });

        //    });
        //    services.AddMassTransitHostedService();

        //}
    }
}
