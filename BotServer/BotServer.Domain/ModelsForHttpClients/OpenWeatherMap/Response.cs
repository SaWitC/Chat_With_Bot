using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.ModelsForHttpClients.OpenWeatherMap
{
    public class Response
    {
        public Weather[] weather { get; set; }
    }
}
