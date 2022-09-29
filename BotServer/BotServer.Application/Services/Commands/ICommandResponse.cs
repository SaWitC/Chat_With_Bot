namespace BotServer.Application.Services.Commands
{
    public interface ICommandResponse
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
