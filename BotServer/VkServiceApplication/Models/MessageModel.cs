using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace VkServiceApplication.Models
{
    public class MessageModel 
    {
        public string Text { get; set; }

        public long? VkId { get; set; }
    }
}
