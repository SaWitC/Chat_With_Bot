using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MassTransit;
using BotServer.Domain.ComuinicationModels;
using ServerApp.Rabitmq;

namespace BotServer.Comunications
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSection = configuration.GetSection("MassTransit");
            var url = configuration["MassTransit:Url"];
            var host = configuration["MassTransit:Host"];
            var userName = configuration["MassTransit:UserName"];
            var password = configuration["MassTransit:Password"];
            if (
                string.IsNullOrEmpty(url) ||
                string.IsNullOrEmpty(host) ||
                string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(password)
                )
            {
                throw new ArgumentNullException("Section 'mass-transit' configuration settings are not found in appSettings.json");
            }

            services.AddMassTransit(x =>
            {
                x.AddBus(bf =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                        {
                            configurator.Username(userName);
                            configurator.Password(password);
                        });

                        cfg.ClearMessageDeserializers();
                        cfg.UseRawJsonSerializer();
                        //cfg.UseHealthCheck(context);
                        cfg.ConfigureEndpoints(bf, SnakeCaseEndpointNameFormatter.Instance);

                        //cfg.ReceiveEndpoint("MyQueue", e =>
                        //{
                        //    //Task.Delay(5000);
                        //    e.Consumer<MyCustomConsumer>();
                        //});
                    });
                    return bus;
                });

            });

            services.AddMassTransitHostedService();
        }
    }
}
