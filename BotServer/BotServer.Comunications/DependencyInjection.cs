using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MassTransit;
using BotServer.Domain.ComuinicationModels;

namespace BotServer.Comunications
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //var massTransitSection = configuration.GetSection("MassTransit");
            var url = configuration["MassTransit:Url"];
            var host = configuration["MassTransit:Host"];
            var userName = configuration["MassTransit:UserName"];
            var password = configuration["MassTransit:Password"];

            //var host = massTransitSection.GetValue<string>("Host");
            //var userName = massTransitSection.GetValue<string>("UserName");
            //var password = massTransitSection.GetValue<string>("Password");
            if (string.IsNullOrEmpty(url)||
                string.IsNullOrEmpty(host) ||
                string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("can't get value from MassTransit section ");
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

                        //cfg.ReceiveEndpoint("MyQueue"e =>
                        //{
                        //    e.Consumer<My>
                        //})
                    });
                    return bus;
                });
                //x.SetSnakeCaseEndpointNameFormatter();

                //x.UsingRabbitMq((context, cfg) =>
                //{
                //    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                //    {
                //        configurator.Username(userName);
                //        configurator.Password(password);
                //    });

                //    cfg.ClearMessageDeserializers();
                //    cfg.UseRawJsonSerializer();
                //    //cfg.UseHealthCheck(context);
                //    cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
                //});
                
            });

            //services.AddMassTransit();
            services.AddMassTransitHostedService();
        }
    }
}
