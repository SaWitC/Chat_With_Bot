namespace ServerApp.Models
{
    public class MessageModel : IMessage
    {
        public string Text { get; set; }

        public long? VkId { get; set; }
    }
}
