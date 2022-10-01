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


                //x.AddConsumer<MyCustomConsumer, MyCustomConsumerDiffenition>();
                //x.UsingRabbitMq((ctx, cfg) =>
                //{
                //    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                //    {
                //        configurator.Username(userName);
                //        configurator.Password(password);
                //    });

                //    cfg.ReceiveEndpoint("MyQueue", e =>
                //    {
                //        e.ConfigureConsumer<MyCustomConsumer>(ctx);
                //    });

                //    cfg.ClearMessageDeserializers();
                //    cfg.UseRawJsonSerializer();
                //    //cfg.ConfigureEndpoints(cfg);
                //    //cfg.UseHealthCheck(context);
                //    //cfg.ConfigureEndpoints(bf, SnakeCaseEndpointNameFormatter.Instance);

                //});


                //x.AddBus(bf =>
                //{
                    x.UsingRabbitMq((ctx,cfg) =>
                    {
                        

                        cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                        {
                            configurator.Username(userName);
                            configurator.Password(password);
                        });

                        cfg.ClearMessageDeserializers();
                        cfg.UseRawJsonSerializer();
                        ///cfg.ConfigureEndpoints(cfg);
                        //cfg.UseHealthCheck(context);
                        //cfg.ConfigureEndpoints(bf, SnakeCaseEndpointNameFormatter.Instance);

                        //cfg.ReceiveEndpoint("MyQueue", e =>
                        //{
                        //    //Task.Delay(5000);
                        //    e.Consumer<MyCustomConsumer>();
                        //});

                        cfg.ReceiveEndpoint("MyQueue",e =>
                        {
                            //Task.Delay(5000);
                            e.ConfigureConsumer<MyCustomConsumer>(ctx);
                        });

                    });
                x.AddConsumer<MyCustomConsumer>(typeof(MyCustomConsumerDiffenition));
                //return bus;
            });
                

            //});
           
            services.AddMassTransitHostedService();

        }
    }
}
