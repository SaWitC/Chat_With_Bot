using BotServer.Domain.ComuinicationModels;
using MassTransit;

namespace ServerApp.Rabitmq
{
    public class MyCustomConsumer : IConsumer<ComunicationMessage>
    {
        public Task Consume(ConsumeContext<ComunicationMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            return Task.CompletedTask;
        }
    }
}
