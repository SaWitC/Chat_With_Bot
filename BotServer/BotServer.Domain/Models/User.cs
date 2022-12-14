namespace BotServer.Domain.Models
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public long? VkId { get; set; }
        public string? VkEmail { get; set; }
        public bool SendToVk { get; set; }
    }
}
