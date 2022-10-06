using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BotServer.Domain.ConfigModels
{
    public class AuthOptions
    {
        public string Issuer { get; set; }

        public string[] Audience { get; set; }

        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }

        public SymmetricSecurityKey GetSymetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
