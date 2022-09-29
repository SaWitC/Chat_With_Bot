namespace BotServer.Application.Services.Commands
{
    public interface ICommandHandler
    {
        Task<string> ProcessCommand(ICommand command);
        bool CanProcess(ICommand command);
    }
}
