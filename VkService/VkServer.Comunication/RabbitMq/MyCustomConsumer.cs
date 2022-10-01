using MassTransit;
using Microsoft.Extensions.Configuration;
using ServerApp.Models;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ServerApp.Rabitmq
{
    public class MyCustomConsumer : IConsumer<MessageModel>
    {
        private readonly IConfiguration _config;

        public MyCustomConsumer(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task Consume(ConsumeContext<MessageModel> context)
        {
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    VkApi vkApi = new VkApi();
                    vkApi.Authorize(new ApiAuthParams()
                    {
                        AccessToken = _config["VkConfig:Token"]
                    });
                    await vkApi.Messages.SendAsync(new MessagesSendParams()
                    {
                        UserId = context.Message.VkId,
                        Message = context.Message.Text,
                        RandomId = Environment.TickCount
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error vkid incorrect");
            }


            Console.WriteLine(context.Message.Text);
            Console.WriteLine(context.Message.VkId);

            //  Task.CompletedTask;
        }
    }

    public class MyCustomConsumerDiffenition:ConsumerDefinition<MyCustomConsumer>
    {
        public MyCustomConsumerDiffenition()
        {
            EndpointName = "MyQueue";
            ConcurrentMessageLimit = 3;
            
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
           IConsumerConfigurator<MyCustomConsumer> consumerConfigurator)
        {
            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            // use the outbox to prevent duplicate events from being published
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
