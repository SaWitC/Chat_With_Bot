﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.CustomHTTPClients
{
    public interface IWeatherHttpClient
    {
        Task GetWeather(string location);
    }
}