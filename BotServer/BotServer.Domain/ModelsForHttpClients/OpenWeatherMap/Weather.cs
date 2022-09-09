using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.ModelsForHttpClients.OpenWeatherMap
{
    public class Weather
    {  
        public string id{ get;set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get;set; }
    }
}

