using MassTransit;
using VkServiceApplication.Handlers;

namespace VkServiceApplication
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
                x.UsingRabbitMq((ctx, cfg) =>
                {


                    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                    {
                        configurator.Username(userName);
                        configurator.Password(password);
                    });

                    cfg.ClearMessageDeserializers();
                    cfg.UseRawJsonSerializer();

                    cfg.ReceiveEndpoint("MyQueue", e =>
                    {
                        //Task.Delay(5000);
                        e.ConfigureConsumer<MyCustomConsumer>(ctx);
                    });

                });
                x.AddConsumer<MyCustomConsumer>(typeof(MyCustomConsumerDiffenition));
            });

            services.AddMassTransitHostedService();

        }
    }
}
