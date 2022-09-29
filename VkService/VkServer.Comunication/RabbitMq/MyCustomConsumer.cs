using MassTransit;
using ServerApp.Models;

namespace ServerApp.Rabitmq
{
    public class MyCustomConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            return Task.CompletedTask;
        }
    }
}
