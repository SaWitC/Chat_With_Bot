using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerApp.Rabitmq;
using MassTransit.RabbitMqTransport;

namespace VkServer.Comunication
{
    public class DependencyInjection
    {
        public async static Task ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSection = configuration.GetSection("MassTransit");
            var url = configuration["MassTransit:Url"];
            var host = configuration["MassTransit:Host"];
            var userName = configuration["MassTransit:UserName"];
            var password = configuration["MassTransit:Password"];
            if (
                string.IsNullOrEmpty(url)||
                string.IsNullOrEmpty(host)||
                string.IsNullOrEmpty(userName)||
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

                        cfg.ReceiveEndpoint("MyQueue", e =>
                        {
                            e.Consumer<MyCustomConsumer>();
                        });
                    });
                    return bus;
                });

            });
           
            services.AddMassTransitHostedService();

        }
    }
}
